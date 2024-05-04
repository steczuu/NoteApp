using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApp.Viewmodels
{
    public class SearchBarVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        NoteListVM listVM = new NoteListVM();
        private IList<string> searchResult;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SearchCommand { get; set; }

        public SearchBarVM()
        {
            SearchCommand = new Command<string>((string query) =>
            {
                
            });

            searchResult = (IList<string>)listVM.notes;
        }

        public IList<string> SearchResult
        {
            get
            {
                return searchResult;
            }
            set
            {
                searchResult = value;
                OnPropertyChanged();    
            }
        }

    }
}
