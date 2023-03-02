using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class PasswordForgotPage : ContentPage
{
	public PasswordForgotPage(PasswordForgotViewModel passwordForgotViewModel)
	{
		InitializeComponent();
		BindingContext = passwordForgotViewModel;
	}
}