﻿using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarListApp.Maui.Services;

public class CarServiceApi : ICarServiceApi
{
    HttpClient _httpClient;
	static string BaseAddress = "https://carlist-api.azurewebsites.net";
    public string StatusMessage;


    public CarServiceApi()
	{
		_httpClient= new HttpClient();
		_httpClient.BaseAddress = new Uri(BaseAddress);
	}


    #region Car Endpoints

    public async Task<List<Car>> GetCars()
	{
        try
        {
            await SetAuthToken();
            var response = await _httpClient.GetStringAsync("/cars");
            var cars = JsonSerializer.Deserialize<List<Car>>(response);
            return cars;
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return null;
    }

    public async Task<Car> GetCar(int id)
	{
        try
        {
            var response = await _httpClient.GetStringAsync($"/cars/{id}");
            var car = JsonSerializer.Deserialize<Car>(response);
            return car;
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to retrieve data.";
        }

        return null;
    }

    public async Task AddCar(Car car)
	{
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"/cars", car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Insert Succesful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Insert data.";
        }
    }

    public async Task UpdateCar(int id, Car car)
	{
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/cars/{id}", car);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Update Succesful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Update data.";
        }
    }

    public async Task DeleteCar(int id)
	{
        try
        {
            var response = await _httpClient.DeleteAsync($"/cars/{id}");
            response.EnsureSuccessStatusCode();
            StatusMessage = "Delete Succesful";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to Delete data.";
        }
    }


    public async Task<string> GetStatusMessage()
    {
        return await Task.FromResult(StatusMessage);
    }

    #endregion


    #region Auth Endpoints

    public async Task<AuthResponseModel> Login(LoginModel loginModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/auth/login", loginModel);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Login Successful";

            var responseContentString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AuthResponseModel>(responseContentString);

        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to login successfully.";
            return null;
        }
    }

    public async Task SetAuthToken()
    {
        var token = await SecureStorage.GetAsync("Token");

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    public async Task Register(RegisterModel registerModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/auth/register", registerModel);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Registration Succesful.";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to register.";
        }
    }

    public async Task PasswordRecovery(string email)
    {
        try
        {
            var response = await _httpClient.PostAsync($"/auth/forgot?email={email}", null);
            response.EnsureSuccessStatusCode();
            StatusMessage = "Password Recovery Email has been sent.";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to send Password Recovery mail.";
        }
    }

    #endregion

}
