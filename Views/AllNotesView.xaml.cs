using Microsoft.Maui.Controls.Platform;

namespace NoteApp.Views
{
    public partial class AllNotesView : ContentPage
    {
        public AllNotesView()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("Alert", "Object selected", "OK");
        }
    }

}
