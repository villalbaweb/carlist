using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using System.Collections.ObjectModel;

namespace CarListApp.Maui.ViewModels;

public partial class CarListViewModel : BaseViewModel
{
	const string EDIT_BTN_TXT = "Update Car";
	const string CREATE_BTN_TXT = "Add Car";

	private readonly CarService _carService;
	private bool isUpdateMode;
	private int carId;


	public CarListViewModel(CarService carService)
	{
		Title = "CarList";

		_carService = carService;
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
			await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "OK");
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
			_carService.UpdateCar(car);
		}
		else
		{
            _carService.AddCar(car);
        }

        await Shell.Current.DisplayAlert("Info", _carService.StatusMessage, "OK");
		await GetCarsAsync();
    }

    [RelayCommand]
	async Task DeleteCarAsync(int id)
	{
		if(id==0)
		{
            await Shell.Current.DisplayAlert("Invalid Id", "Please try again", "OK");
        }

		int result = _carService.DeleteCar(id);

		if (result == 0)
            await Shell.Current.DisplayAlert("Deletion Failed", "Please insert valid data", "OK");
		else
		{
            await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "OK");
			await GetCarsAsync();
        }
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

			var cars = _carService.GetCars();
			foreach(var car in cars)
			{
				Cars.Add(car);
			}

		}
		catch (Exception ex)
		{
			await Shell.Current.DisplayAlert("Error", "Failed to retrieve list of cars.", "OK");	// Daniel: Move this away to an abstraction the ViewModel should not be aware of the View
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

		await Shell.Current.GoToAsync(		// Probably this can be abstracted away to a Navigation Interface
			$"{ nameof(CarDetailsPage) }?Id={id}", 
			true);
	}

    [RelayCommand]
    async Task SetEditModeAsync(int id)
    {
		if (id == 0) return;

		isUpdateMode = true;

		AddUpdateModeText = EDIT_BTN_TXT;

        Car car = _carService.GetCar(id);

		carId = car.Id;
		Make = car.Make;
		Model = car.Model;
		Vin = car.Vin;

    }
}