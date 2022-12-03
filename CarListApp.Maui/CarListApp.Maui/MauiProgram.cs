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
		builder.Services.AddTransient<CarDetailsViewModel>();

		// Pages
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<CarDetailsPage>();

		return builder.Build();
	}
}
