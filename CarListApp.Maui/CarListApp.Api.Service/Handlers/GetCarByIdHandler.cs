using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Service.Queries;
using MediatR;

namespace CarListApp.Api.Service.Handlers;

public class GetCarByIdHandler : IRequestHandler<GetCarByIdQuery, Car>
{
    #region Private Members

    private readonly ICarRepository _carRepository;

    #endregion


    #region Constructor

    public GetCarByIdHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    #endregion


    #region Public Method

    public async Task<Car> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        return await _carRepository.GetCarByIdAsync(request.Id);
    }

    #endregion
}
