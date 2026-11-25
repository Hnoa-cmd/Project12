using Project12.ViewModels;

namespace Project12.Views;

public partial class SignInPage : ContentPage
{
    public SignInPage()
    {
        InitializeComponent();
        BindingContext = new SignInViewModel();
    }

    private async void OnSignUpTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}