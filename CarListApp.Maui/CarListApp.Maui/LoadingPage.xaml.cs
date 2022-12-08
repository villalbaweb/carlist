using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui;

public partial class LoadingPage : ContentView
{
	public LoadingPage(LoadingPageViewModel loadingPageViewModel)
	{
		InitializeComponent();
		BindingContext= loadingPageViewModel;
	}
}