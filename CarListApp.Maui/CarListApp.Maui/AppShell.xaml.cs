using CarListApp.Maui.Views;

namespace CarListApp.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// This is just top Register a new Navigation Route within the AppShell
		Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
	}
}
