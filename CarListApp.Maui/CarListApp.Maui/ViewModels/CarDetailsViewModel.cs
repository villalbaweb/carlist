using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;

namespace CarListApp.Maui.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly CarService _carService;


    [ObservableProperty]
    Car car;

    [ObservableProperty]
    int id;

    public CarDetailsViewModel(CarService carService)
    {
        _carService= carService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
        Car = _carService.GetCar(id);
    }
}
