using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Service.Commands;
using MediatR;

namespace CarListApp.Api.Service.Handlers;

public class UpdateCarHandler : IRequestHandler<UpdateCarCommand, bool>
{
    #region Private Members

    private readonly ICarRepository _carRepository;

    #endregion


    #region Constructor

    public UpdateCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    #endregion


    #region Public Methods

    public async Task<bool> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        return await _carRepository.UpdateCarAsync(request.Id, new Core.ValueObjects.Car
        {
            Id = request.Id,
            Make = request.Make,
            Model = request.Model,
            Vin = request.Vin
        });
    }

    #endregion
}
