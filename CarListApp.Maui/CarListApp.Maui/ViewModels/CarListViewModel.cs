using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
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

	[RelayCommand]
	async Task GetCarsAsync()
	{
		if (IsLoading) return;

		try
		{
			IsLoading = true;

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
		}
	}
}