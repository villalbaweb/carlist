using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteCarAsync(int id)
    {
        var record = await _dbContext.Cars.FindAsync(id);
        if(record is not null)
        {
            _dbContext.Remove(record);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<Car> GetCarByIdAsync(int id)
    {
        return await _dbContext.Cars.FindAsync(id);
    }

    public async Task<List<Car>> GetCarListAsync()
    {
        return await _dbContext.Cars.ToListAsync();
    }

    public async Task UpdateCarAsync(int id, Car car)
    {
        var record = await _dbContext.Cars.FindAsync(id);
        if(record is not null)
        {
            record.Model = car.Model;
            record.Make = car.Make;
            record.Vin = car.Vin;

            await _dbContext.SaveChangesAsync();
        }
    }

    #endregion
}
