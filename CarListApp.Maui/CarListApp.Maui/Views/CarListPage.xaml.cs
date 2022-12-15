using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarListPage : ContentPage
{
	public CarListPage(CarListViewModel carListViewModel)
	{
		InitializeComponent();
		BindingContext = carListViewModel;
	}
}