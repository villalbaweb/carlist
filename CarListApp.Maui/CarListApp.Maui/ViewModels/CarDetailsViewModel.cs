using CarListApp.Maui.Interfaces.Services;
using CarListApp.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Web;

namespace CarListApp.Maui.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
{


    private readonly ICarServiceApi _carServiceApi;


    [ObservableProperty]
    Car car;

    [ObservableProperty]
    int id;

    public CarDetailsViewModel(ICarServiceApi carServiceApi)
    {
        _carServiceApi= carServiceApi;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Id = Convert.ToInt32(HttpUtility.UrlDecode(query[nameof(Id)].ToString()));
    }

    /// <summary>
    /// Created so that it can be called from the view itself 
    /// OnAppearing() this is required to make the CarServiceApi call async, that cannot be don
    /// with in the ApplyQueryAttributes since its pretty much synchronous.
    /// </summary>
    /// <returns></returns>
    public async Task GetCarData()
    {
        Car = await _carServiceApi.GetCar(Id);
    }
}
