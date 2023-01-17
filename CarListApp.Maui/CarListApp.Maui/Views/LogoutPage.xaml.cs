using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage(LogoutPageViewModel logoutPageViewModel)
	{
		InitializeComponent();
		BindingContext = logoutPageViewModel;
	}
}