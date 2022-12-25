
using CarListApp.Api.Core.ValueObjects;

namespace CarListApp.Api.Core.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarListAsync();

    Task<Car> FindCarByIdAsync(int id);

    Task UpdateCarAsync(int id, Car car);

    Task DeleteCarAsync(int id);

    Task<Car> AddCarAsync(Car car);
}
