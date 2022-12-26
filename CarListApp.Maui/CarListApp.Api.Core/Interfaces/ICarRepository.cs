
using CarListApp.Api.Core.ValueObjects;

namespace CarListApp.Api.Core.Interfaces;

public interface ICarRepository
{
    Task<List<Car>> GetCarListAsync();

    Task<Car> GetCarByIdAsync(int id);

    Task<bool> UpdateCarAsync(int id, Car car);

    Task<bool> DeleteCarAsync(int id);

    Task<Car> AddCarAsync(Car car);
}
