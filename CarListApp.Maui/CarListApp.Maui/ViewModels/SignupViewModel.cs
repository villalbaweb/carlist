using CarListApp.Maui.Interfaces.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels;

public partial class SignupViewModel : BaseViewModel
{
    private readonly IDisplayAlertService _displayAlertService;

    public SignupViewModel(
        IDisplayAlertService displayAlertService)
    {
        _displayAlertService = displayAlertService;
    }

    [ObservableProperty]
    string username;

    [ObservableProperty]
    string email;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    string confirmPassword;

    [RelayCommand]
    async Task SignUpAsync()
    {
        bool isSignUpDataComplete = await IsSignUpCompleteData();

        if(isSignUpDataComplete)
        {
            await SignUpAlertDisplay("Proceed with Sign Up request...");
        }
    }


    private async Task<bool> IsSignUpCompleteData()
    {
        if (string.IsNullOrEmpty(Username))
        {
            await SignUpAlertDisplay("Please enter Username.");
            return await Task.FromResult(false);
        }

        if (string.IsNullOrEmpty(Email))
        {
            await SignUpAlertDisplay("Please enter Email.");
            return await Task.FromResult(false);
        }

        if (string.IsNullOrEmpty(Password))
        {
            await SignUpAlertDisplay("Please enter Password.");
            return await Task.FromResult(false);
        }

        if (string.IsNullOrEmpty(ConfirmPassword))
        {
            await SignUpAlertDisplay("Please enter Password Confirmation.");
            return await Task.FromResult(false);
        }

        return await Task.FromResult(true);
    }

    private async Task SignUpAlertDisplay(string message)
    {
        await _displayAlertService.DisplayAlertAsync("Sign Up Page", message, "OK");
    }
}
