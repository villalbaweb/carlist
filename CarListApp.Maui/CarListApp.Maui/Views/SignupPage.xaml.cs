using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class SignupPage : ContentPage
{
	public SignupPage(SignupViewModel signupViewModel)
	{
		InitializeComponent();
		BindingContext = signupViewModel;
	}
}