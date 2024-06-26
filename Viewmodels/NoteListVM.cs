﻿using NoteApp.Models;
using NoteApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.Viewmodels
{
    public class NoteListVM
    {
        NoteVM note_VM;
        bool isEditing;
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly NoteModelContext noteModelContext = new NoteModelContext();
        public Categories _selectedCategory;

        public bool IsEditing
        {
            private set { SetProperty(ref isEditing, value); }
            get { return isEditing; }
        }

        public NoteVM Note_VM
        {
            set { SetProperty(ref note_VM, value); }
            get { return note_VM; }
        }

        public Categories SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                SetProperty(ref _selectedCategory, value);
                Note_VM.Category = _selectedCategory.Value;
            }
        }

        public ICommand AddCommand { private set; get; }
        public ICommand AddNewNoteForm {  private set; get; }

        public IList<NoteVM> notes { get; set; } = new ObservableCollection<NoteVM>();
        public List<Categories> CategoriesList { get; set; }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NoteListVM()
        {
            LoadNotes();

            CategoriesList = PickerService.GetCategories().OrderBy(c => c.Value).ToList();

            AddNewNoteForm = new Command(
            execute: () =>
            {
                Note_VM = new NoteVM();
                Note_VM.PropertyChanged += OnNotePropertyChanged;
                IsEditing = true;
            },
            canExecute: () =>
            {
                return !IsEditing;
            });

            void OnNotePropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                (AddCommand as Command).ChangeCanExecute();              
            }

            void RefreshCanExecutes()
            {
                (AddCommand as Command).ChangeCanExecute();
            }

            AddCommand = new Command(execute: () =>
            { 
                notes.Add(Note_VM);
                Note_VM.PropertyChanged -= OnNotePropertyChanged;
                IsEditing = false;
                RefreshCanExecutes();


                var note = new NoteModel
                {
                    Title = Note_VM.Title,  
                    Category = Note_VM.Category,    
                    NoteDate = Note_VM.Date,    
                    NoteInput = Note_VM.Note
                };

                noteModelContext.Notes.Add(note);
                noteModelContext.SaveChanges();
                Note_VM = null;
            },
            canExecute: () =>
            {
                return Note_VM != null &&
                       Note_VM.Title != null &&
                       Note_VM.Note != null &&
                       Note_VM.Category != null;
            });
        }

        private void LoadNotes()
        {
            using (var noteModelContext = new NoteModelContext())
            {
                var notesFromDB = noteModelContext.Notes;

                foreach (var note in notesFromDB)
                {
                    notes.Add(new NoteVM
                    {
                        Title = note.Title,
                        Category = note.Category,
                        Date = note.NoteDate,
                        Note = note.NoteInput
                    });
                }
            }
        }

        public void DeleteNote(NoteVM note)
        {
            notes.Remove(note);

            var noteToRemove = noteModelContext.Notes.FirstOrDefault(n => n.Title == note.Title && n.Category == note.Category);
            if (noteToRemove != null)
            {
                noteModelContext.Notes.Remove(noteToRemove);
                noteModelContext.SaveChanges();
            }
        }
    }
}
