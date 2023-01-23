using CarListApp.Maui.Interfaces.Helpers;
using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels;

public partial class LogoutPageViewModel : BaseViewModel
{
	private readonly IUserInfoHelper _userInfoHelper;
    private readonly INavigationService _navigationService;

    public LogoutPageViewModel(
		IUserInfoHelper userInfoHelper,
        INavigationService navigationService)
	{
		_userInfoHelper = userInfoHelper;
		_navigationService = navigationService;

		Logout();
	}

	[RelayCommand]
	async void Logout()
	{
		SecureStorage.Remove("Token");
		_userInfoHelper.ClearUserInfoPreferences();
		await _navigationService.NavigateToAsync($"{nameof(LoginPage)}");
	}
}
