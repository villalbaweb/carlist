namespace CarListApp.Maui.ViewModels;

public partial class LoadingPageViewModel : BaseViewModel
{
	public LoadingPageViewModel()
	{
		CheckUserLoginDetails();
	}

    private async void CheckUserLoginDetails()
    {
        // Retrieve token from internal storage
        var token = await SecureStorage.GetAsync("Token");

        if(string.IsNullOrEmpty(token))
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        // Evaluate token and decide if valid

    }

    private async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    private async Task GoToMainPage()
    {
        await Shell.Current.GoToAsync($"{nameof(MainPage)}");
    }
}
