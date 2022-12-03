using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarDetailsPage : ContentPage
{
	private readonly CarDetailsViewModel _carDetailsViewModel;

	public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
	{
		InitializeComponent();
		BindingContext= carDetailsViewModel;

		_carDetailsViewModel= carDetailsViewModel;
	}

	// Daniel: Used the ONAppearing() event to asynchronouslly get the Car Data

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _carDetailsViewModel.GetCarData();
    }
}