﻿using CarListApp.Maui.Interfaces.Helpers;
using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Models;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarListApp.Maui.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly ICarServiceApi _carServiceApi;
    private readonly IUserInfoHelper _userInfoHelper;
    private readonly IMenuBuildHelper _menuBuildHelper;
    private readonly INavigationService _navigationService;
    private readonly IDisplayAlertService _displayAlertService;

    public LoginViewModel(
        ICarServiceApi carServiceApi,
        IUserInfoHelper userInfoHelper,
        IMenuBuildHelper menuBuildHelper,
        INavigationService navigationService,
        IDisplayAlertService displayAlertService)
    {
        _carServiceApi = carServiceApi;
        _userInfoHelper = userInfoHelper;
        _menuBuildHelper = menuBuildHelper;
        _navigationService = navigationService;
        _displayAlertService = displayAlertService;
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
            await DisplayLoginMessage(await _carServiceApi.GetStatusMessage());

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

                _userInfoHelper.SetUserInfoToPreferences(userInfo);

                // navigate to apps main page
                _menuBuildHelper.BuildMenu();
                await _navigationService.NavigateToAsync($"{nameof(CarListPage)}");
            }
            else
                await DisplayLoginMessage("Invalid Login Attempt");
        }
    }

    [RelayCommand]
    async Task NavigateToSignUpAsync()
    {
        await _navigationService.NavigateToAsync($"{nameof(SignupPage)}");
    }

    [RelayCommand]
    async Task PasswordForgottenAsync()
    {
        await _navigationService.NavigateToAsync($"{nameof(PasswordForgotPage)}");
    }

    private async Task DisplayLoginMessage(string message)
    {
        await _displayAlertService.DisplayAlertAsync("Login Attempt Result", message, "OK");
        Password = string.Empty;
    }
}
