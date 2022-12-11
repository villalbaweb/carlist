using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext= loadingPageViewModel;
	}
}