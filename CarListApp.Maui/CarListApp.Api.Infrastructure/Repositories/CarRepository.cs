using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.ValueObjects;

namespace CarListApp.Api.Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    #region Private Members

    CarListDbContext _dbContext;

    #endregion

    #region Constructor

    public CarRepository(CarListDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Public Methods

    public async Task<Car> AddCarAsync(Car car)
    {
        await _dbContext.AddAsync(car);
        await _dbContext.SaveChangesAsync();

        return car;
    }

    public Task DeleteCarAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Car> FindCarByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Car>> GetCarListAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateCarAsync(int id, Car car)
    {
        throw new NotImplementedException();
    }

    #endregion
}
