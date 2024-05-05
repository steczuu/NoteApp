using Microsoft.Maui.Controls.Platform;
using NoteApp.Viewmodels;

namespace NoteApp.Views
{
    public partial class AllNotesView : ContentPage
    {
        private bool WasClicked = true;


        public AllNotesView()
        {
            InitializeComponent();
            BindingContext = new NoteListVM();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("Alert", "Object selected", "OK");
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

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
