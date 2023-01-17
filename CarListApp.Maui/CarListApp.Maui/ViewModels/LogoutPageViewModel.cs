using CarListApp.Maui.Interfaces.Helpers;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.Input;

namespace CarListApp.Maui.ViewModels;

public partial class LogoutPageViewModel : BaseViewModel
{
	private readonly IUserInfoHelper _userInfoHelper;

	public LogoutPageViewModel(IUserInfoHelper userInfoHelper)
	{
		_userInfoHelper = userInfoHelper;

		Logout();
	}

	[RelayCommand]
	async void Logout()
	{
		SecureStorage.Remove("Token");
		_userInfoHelper.ClearUserInfoPreferences();
		await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
	}
}
