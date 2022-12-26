using CarListApp.Api.Core.ValueObjects;
using MediatR;

namespace CarListApp.Api.Service.Queries;

public class GetCarByIdQuery : IRequest<Car>
{
    public int Id { get; set; }
}
