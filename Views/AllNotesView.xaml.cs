using NoteApp.Viewmodels;
using System.Diagnostics;


namespace NoteApp.Views
{
    public partial class AllNotesView : ContentPage
    {
        private bool WasClicked = true;
        public bool answer;

        NoteListVM listVM;
        public AllNotesView()
        {
            InitializeComponent();
            listVM = new NoteListVM();
            BindingContext = listVM;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var selectedNote = e.SelectedItem as NoteVM;
            answer = await DisplayAlert("Alert", "Delete selected note?", "Yes", "No");

            if (answer)
            {
                listVM.DeleteNote(selectedNote);
            }

            NotesList.SelectedItem = null;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (WasClicked)
            {
                NotesList.TranslateTo(0, 0, 2500);
                await AddTxt.FadeTo(1, 500);
                await TitleInput.FadeTo(1, 700);
                await CategoryPicker.FadeTo(1, 500);    
                await NoteInput.FadeTo(1, 700); 
                await NoteAddBtn.FadeTo(1, 700);  

                WasClicked = false;
            }
            else
            {
                NotesList.TranslateTo(0, -800, 2500);
                await NoteAddBtn.FadeTo(0, 500);
                await NoteInput.FadeTo(0, 700);
                await CategoryPicker.FadeTo(0, 500);
                await TitleInput.FadeTo(0, 700);
                await AddTxt.FadeTo(0, 700);

                WasClicked = true;
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                NotesList.ItemsSource = listVM.notes;
            }
            else
            {
                NotesList.ItemsSource = listVM.notes.Where(c => c.Title.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        private void NoteAddBtn_Clicked(object sender, EventArgs e)
        {
            listVM.Note_VM = new NoteVM();
            NoteInput.Text = string.Empty;
            TitleInput.Text = string.Empty;
        }
    }

}
