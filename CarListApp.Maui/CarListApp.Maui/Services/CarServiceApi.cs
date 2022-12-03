using CarListApp.Maui.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarListApp.Maui.Services;

public class CarServiceApi
{
    HttpClient _httpClient;
	static string BaseAddress = "https://carlist-api.azurewebsites.net";
    public string StatusMessage;


    public CarServiceApi()
	{
		_httpClient= new HttpClient();
		_httpClient.BaseAddress = new Uri(BaseAddress);
	}

	public async Task<List<Car>> GetCars()
	{
        try
        {
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
}
