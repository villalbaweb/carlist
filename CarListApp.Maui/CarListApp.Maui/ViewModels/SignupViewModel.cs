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
        await _displayAlertService.DisplayAlertAsync("Sign Up Page", "Sign Up button clicked...", "OK");
    }
}
