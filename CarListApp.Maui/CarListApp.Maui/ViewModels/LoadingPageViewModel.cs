using CarListApp.Maui.Helpers;
using CarListApp.Maui.Interfaces.Helpers;
using CarListApp.Maui.Models;
using CarListApp.Maui.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarListApp.Maui.ViewModels;

public partial class LoadingPageViewModel : BaseViewModel
{
    private readonly IMenuBuildHelper _menuBuildHelper;
    private readonly IUserInfoHelper _userInfoHelper;

	public LoadingPageViewModel(
        IMenuBuildHelper menuBuildHelper,
        IUserInfoHelper userInfoHelper)
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
                await GoToCarListPage();
            }
        }

        // Evaluate token and decide if valid

    }

    private async Task GoToLoginPage()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }

    private async Task GoToCarListPage()
    {
        await Shell.Current.GoToAsync($"{nameof(CarListPage)}");
    }
}
