using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Viewmodels
{
    public class NoteVM : INotifyPropertyChanged
    {
        string title;
        string note;
        string category;
        DateTime date = DateTime.Now;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Title
        {
            set { SetProperty(ref title, value); }
            get { return title; }   
        }

        public string Note
        {
            set { SetProperty(ref note, value); }
            get { return note; }    
        }

        public string Category
        {
            set { SetProperty(ref category, value); }
            get { return category; }
        }

        public DateTime Date
        {
            set { SetProperty(ref date, value); }
            get { return date; }
        }

        public override string ToString()
        {
            return Title + Note + Category + Date;
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
