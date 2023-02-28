using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Models;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels;

public partial class SignupViewModel : BaseViewModel
{
    private readonly ICarServiceApi _carServiceApi;
    private readonly INavigationService _navigationService;
    private readonly IDisplayAlertService _displayAlertService;

    public SignupViewModel(
        ICarServiceApi carServiceApi,
        INavigationService navigationService,
        IDisplayAlertService displayAlertService)
    {
        _carServiceApi = carServiceApi;
        _navigationService = navigationService;
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

            RegisterModel registerModel = new RegisterModel
            {
                Email = Email,
                Username = Username,
                Password = Password,
                ConfirmPassword = ConfirmPassword,
                Role = "User"
            };

            await _carServiceApi.Register(registerModel);
            await SignUpAlertDisplay(await _carServiceApi.GetStatusMessage());

            await _navigationService.NavigateToAsync($"{nameof(LoginPage)}");
        }
    }


    private async Task<bool> IsSignUpCompleteData()
    {
        if (string.IsNullOrEmpty(Username))
        {
            await SignUpAlertDisplay("Please enter Username.");
            return false;
        }

        if (string.IsNullOrEmpty(Email))
        {
            await SignUpAlertDisplay("Please enter Email.");
            return false;
        }

        if (string.IsNullOrEmpty(Password))
        {
            await SignUpAlertDisplay("Please enter Password.");
            return false;
        }

        if (string.IsNullOrEmpty(ConfirmPassword))
        {
            await SignUpAlertDisplay("Please enter Password Confirmation.");
            return false;
        }

        if (!string.Equals(Password, ConfirmPassword, StringComparison.Ordinal))
        {
            await SignUpAlertDisplay("Confirmation Password does not match.");
            return false;
        }

        return true;
    }

    private async Task SignUpAlertDisplay(string message)
    {
        await _displayAlertService.DisplayAlertAsync("Sign Up Page", message, "OK");
    }
}
