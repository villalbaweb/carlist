using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels;

public partial class PasswordForgotViewModel : BaseViewModel
{
    private readonly ICarServiceApi _carServiceApi;
    private readonly INavigationService _navigationService;
    private readonly IDisplayAlertService _displayAlertService;

    public PasswordForgotViewModel(
        ICarServiceApi carServiceApi,
        INavigationService navigationService,
        IDisplayAlertService displayAlertService)
    {
        _carServiceApi = carServiceApi;
        _navigationService = navigationService;
        _displayAlertService = displayAlertService;
    }

    [ObservableProperty]
    string email;

    [RelayCommand]
    async Task RequestRecoveryMailAsync()
    {
        // Send recovery password email here...
        await _displayAlertService.DisplayAlertAsync("Password Forgotten Page", "Password Change mail has been sent.", "OK");
        await _navigationService.NavigateToAsync($"{nameof(LoginPage)}");
    }
}
