using CarListApp.Maui.Helpers;
using CarListApp.Maui.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarListApp.Maui.ViewModels;

public partial class LoadingPageViewModel : BaseViewModel
{
    private readonly MenuBuildHelper _menuBuildHelper;
    private readonly UserInfoHelper _userInfoHelper;

	public LoadingPageViewModel(
        MenuBuildHelper menuBuildHelper,
        UserInfoHelper userInfoHelper)
	{
        _menuBuildHelper = menuBuildHelper;
        _userInfoHelper = userInfoHelper;

		CheckUserLoginDetails();
	}

    private async Task CheckUserLoginDetails()
    {
        // Retrieve token from internal storage
        string token = await SecureStorage.GetAsync("Token");

        if(string.IsNullOrEmpty(token))
        {
            await GoToLoginPage();
        }
        else
        {
            var jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(token) as JwtSecurityToken;

            if(jsonToken.ValidTo < DateTime.UtcNow)
            {
                SecureStorage.Remove("Token");
                await GoToLoginPage();
            }
            else
            {
                UserInfo userInfo = new UserInfo
                {
                    Username = jsonToken.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Email)).Value,
                    Role = jsonToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value
                };

                _userInfoHelper.SetUserInfoToPreferences(userInfo);
                _menuBuildHelper.BuildMenu();
                await GoToMainPage();
            }
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
