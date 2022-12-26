using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Service.Queries;
using MediatR;

namespace CarListApp.Api.Service.Handlers;

public class GetCarListHandler : IRequestHandler<GetCarListQuery, List<Car>>
{
    #region Private Members

    private readonly ICarRepository _carRepository;

    #endregion


    #region Constructor

    public GetCarListHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    #endregion


    #region Public Method

    public async Task<List<Car>> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
        return await _carRepository.GetCarListAsync();
    }

    #endregion
}
