using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Service.Commands;
using MediatR;

namespace CarListApp.Api.Service.Handlers;

public class DeleteCarHandler : IRequestHandler<DeleteCarCommand, bool>
{
    #region Private Members

    private readonly ICarRepository _carRepository;

    #endregion

    #region Constructor

    public DeleteCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    #endregion


    #region Public Methods

    public async Task<bool> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        return await _carRepository.DeleteCarAsync(request.Id);
    }

    #endregion
}
