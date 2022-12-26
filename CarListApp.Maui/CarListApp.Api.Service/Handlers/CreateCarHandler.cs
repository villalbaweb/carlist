using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Service.Commands;
using MediatR;

namespace CarListApp.Api.Service.Handlers;

public class CreateCarHandler : IRequestHandler<CreateCarCommand, Car>
{
    #region Private Members

    private readonly ICarRepository _carRepository;

    #endregion


    #region Constructor

    public CreateCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    #endregion


    #region Public Methods

    public async Task<Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        Car newCar = new Car
        {
            Make = request.Make,
            Model = request.Model,
            Vin = request.Vin
        };

        return await _carRepository.AddCarAsync(newCar);
    }

    #endregion
}
