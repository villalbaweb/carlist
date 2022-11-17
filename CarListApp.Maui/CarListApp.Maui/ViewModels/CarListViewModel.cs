using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace CarListApp.Maui.ViewModels;

public partial class CarListViewModel : BaseViewModel
{
	private readonly CarService _carService;

	public ObservableCollection<Car> Cars { get; private set; } = new ();

	public CarListViewModel(CarService carService)
	{
		Title = "CarList";

		_carService = carService;
	}

    [ObservableProperty]
    bool isRefreshing;

	[ObservableProperty]
	string make;

	[ObservableProperty]
	string model;

	[ObservableProperty]
	string vin;

	[RelayCommand]
	async Task AddCarAsync()
	{
		if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
		{
			await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "OK");
		}

		Car car = new Car
		{
			Make = Make,
			Model = Model,
			Vin = Vin
		};

		_carService.AddCar(car);
        await Shell.Current.DisplayAlert("Info", _carService.StatusMessage, "OK");
		await GetCarsAsync();
    }

	[RelayCommand]
	async Task UpdateCarAsync(int id)
	{
		await Task.CompletedTask;
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
			Debug.WriteLine($"Unable to get cars: {ex.Message}");
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
}