using NoteApp.Models;
using NoteApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Viewmodels
{
    public class PickerVm : INotifyPropertyChanged
    {
        NoteVM note_VM;
        public event PropertyChangedEventHandler? PropertyChanged;

        public NoteVM Note_VM
        {
            set { SetProperty(ref note_VM, value); }
            get { return note_VM; }
        }

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


        public List<Categories> CategoriesList { get; set; }

        public PickerVm()
        {
            CategoriesList = PickerService.GetCategories().OrderBy(c => c.Value).ToList();
        }

        private Categories _selectedCategory;

        public Categories SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                SetProperty(ref _selectedCategory, value);
                Note_VM.Category = _selectedCategory.ToString();
            }
        }

    }
}
