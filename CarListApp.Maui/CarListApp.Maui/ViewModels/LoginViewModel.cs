using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarListApp.Maui.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly CarServiceApi _carServiceApi;

    public LoginViewModel(CarServiceApi carServiceApi)
    {
        _carServiceApi = carServiceApi;
    }

    [ObservableProperty]
    string username;

    [ObservableProperty]
    string password;

    [RelayCommand]
    async Task LoginAsync()
    {
        if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayLoginMessage("Invalid Login Attempt");
        }
        else
        {
            // Call API to attempt to login
            LoginModel loginModel = new LoginModel
            {
                Username= username,
                Password= password
            };

            var response = await _carServiceApi.Login(loginModel);

                // display welcome message
            await DisplayLoginMessage(_carServiceApi.StatusMessage);

            if (response is not null && !string.IsNullOrEmpty(response.Token))
            {
                // store Token in secuire storage
                await SecureStorage.SetAsync("Token", response.Token);

                // build a menu on the fly... based on the user role
                var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

                UserInfo userInfo = new UserInfo
                {
                    Username = Username,
                    Role = jsonToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role)).Value
                };

                if(Preferences.ContainsKey(nameof(UserInfo)))
                {
                    Preferences.Remove(nameof(UserInfo));
                }

                string userInfoString = JsonSerializer.Serialize(userInfo);
                Preferences.Set(nameof(UserInfo), userInfoString);

                // navigate to apps main page
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
            }
            else
                await DisplayLoginMessage("Invalid Login Attempt");
        }
    }

    private async Task DisplayLoginMessage(string message)
    {
        await Shell.Current.DisplayAlert("Login Attempt Result", message, "OK");
        Password = string.Empty;
    }
}
