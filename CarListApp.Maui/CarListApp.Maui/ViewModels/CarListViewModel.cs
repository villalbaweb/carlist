using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CarListApp.Maui.ViewModels;

public partial class CarListViewModel : BaseViewModel
{
	const string EDIT_BTN_TXT = "Update Car";
	const string CREATE_BTN_TXT = "Add Car";

	private readonly ICarServiceApi _carServiceApi;
    private readonly INavigationService _navigationService;
	private readonly IDisplayAlertService _displayAlertService;

    private bool isUpdateMode;
	private int carId;
	private string message;


	public CarListViewModel(
        ICarServiceApi carServiceApi,
        INavigationService navigationService,
        IDisplayAlertService displayAlertService)
    {
        Title = "CarList";

        _carServiceApi = carServiceApi;
        _navigationService = navigationService;
		_displayAlertService = displayAlertService;

        AddUpdateModeText = CREATE_BTN_TXT;
    }


    public ObservableCollection<Car> Cars { get; private set; } = new ();

    [ObservableProperty]
    bool isRefreshing;

	[ObservableProperty]
	string make;

	[ObservableProperty]
	string model;

	[ObservableProperty]
	string vin;

	[ObservableProperty]
	string addUpdateModeText;

	
	[RelayCommand]
	async Task SaveCarAsync()
	{
		if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
		{
			await _displayAlertService.DisplayAlertAsync("Invalid Data", "Please insert valid data", "OK");
			return;
		}

        Car car = new Car
        {
			Id= carId,
            Make = Make,
            Model = Model,
            Vin = Vin
        };

        if (isUpdateMode)
		{
			await _carServiceApi.UpdateCar(carId, car);
		}
		else
		{
            await _carServiceApi.AddCar(car);
        }

        await _displayAlertService.DisplayAlertAsync("Info", await _carServiceApi.GetStatusMessage(), "OK");
		await GetCarsAsync();
		await ClearForm();
    }

    [RelayCommand]
	async Task DeleteCarAsync(int id)
	{
		if(id==0)
		{
            await _displayAlertService.DisplayAlertAsync("Invalid Id", "Please try again", "OK");
        }

		await _carServiceApi.DeleteCar(id);
		message = await _carServiceApi.GetStatusMessage();

		await _displayAlertService.DisplayAlertAsync("Info", message, "OK");
        await GetCarsAsync();
    }

    [RelayCommand]
	async Task GetCarsAsync()
	{
		if (IsLoading) return;

		try
		{
			IsLoading = true;
			isRefreshing= true;

			if (Cars.Any()) Cars.Clear();

			var cars = await _carServiceApi.GetCars();
			foreach(var car in cars)
			{
				Cars.Add(car);
			}

		}
		catch (Exception ex)
		{
			await _displayAlertService.DisplayAlertAsync("Error", "Failed to retrieve list of cars.", "OK");	// Daniel: Move this away to an abstraction the ViewModel should not be aware of the View
			throw;
		}
		finally
		{
			IsLoading = false;
			isRefreshing = false;
		}
	}

	[RelayCommand]
	async Task GetCarDetailsAsync(int id)
	{
		if(id == 0) return;

		await _navigationService.NavigateToWithAnimationAsync($"{ nameof(CarDetailsPage) }?Id={id}");
	}

    [RelayCommand]
    async Task SetEditModeAsync(int id)
    {
		if (id == 0) return;

		isUpdateMode = true;

		AddUpdateModeText = EDIT_BTN_TXT;

        Car car = await _carServiceApi.GetCar(id);

		carId = car.Id;
		Make = car.Make;
		Model = car.Model;
		Vin = car.Vin;

        await Task.CompletedTask;
    }

	[RelayCommand]
	async Task ClearForm()
	{
		isUpdateMode = false;
		AddUpdateModeText = CREATE_BTN_TXT;
		carId = 0;
		Make = string.Empty;
		Model = string.Empty;
		Vin = string.Empty;

		await Task.CompletedTask;
	}
}