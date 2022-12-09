using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    string username;

    [ObservableProperty]
    string password;

    [RelayCommand]
    async Task LoginAsync()
    {
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayLoginError();
        }
        else
        {
            // Call API to attempt to login
            var loginSuccesful = true;


            if (loginSuccesful)
            {
                // display welcome message

                // build a menu on the fly... based on the user role

                // navigate to apps main page
            }

            await DisplayLoginError();

        }
    }

    private async Task DisplayLoginError()
    {
        await Shell.Current.DisplayAlert("Invalid Attempt", "Invalid Username or Password", "OK");
        Password = string.Empty;
    }
}
