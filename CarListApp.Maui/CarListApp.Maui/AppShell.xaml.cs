using CarListApp.Maui.Views;

namespace CarListApp.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// This is just top Register a new Navigation Route within the AppShell
		Routing.RegisterRoute(nameof(CarDetailsPage), typeof(CarDetailsPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(CarListPage), typeof(CarListPage));
		Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
		Routing.RegisterRoute(nameof(PasswordForgotPage), typeof(PasswordForgotPage));
    }
}
