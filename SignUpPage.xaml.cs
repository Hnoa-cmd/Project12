using Project12.ViewModels;

namespace Project12.Views
{
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = new SignUpViewModel();
        }

        private async void OnSignInTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}