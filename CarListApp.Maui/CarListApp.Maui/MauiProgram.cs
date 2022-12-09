using CarListApp.Maui.Services;
using CarListApp.Maui.ViewModels;
using CarListApp.Maui.Views;

namespace CarListApp.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// SQLite
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");
		
		// Services
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CarServiceDatabase>(s, dbPath));
		builder.Services.AddSingleton<CarServiceApi>();

		// View models
		builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
		builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddTransient<CarDetailsViewModel>();

		// Pages
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddTransient<CarDetailsPage>();

		return builder.Build();
	}
}
