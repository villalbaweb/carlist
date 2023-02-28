using CarListApp.Maui.Models;

namespace CarListApp.Maui.Interfaces.Services;

public interface ICarServiceApi
{
    Task<List<Car>> GetCars();

    Task<Car> GetCar(int id);

    Task AddCar(Car car);

    Task UpdateCar(int id, Car car);

    Task DeleteCar(int id);

    Task<AuthResponseModel> Login(LoginModel loginModel);

    Task SetAuthToken();

    Task<string> GetStatusMessage();
}
