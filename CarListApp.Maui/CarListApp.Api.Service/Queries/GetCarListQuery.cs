using CarListApp.Api.Core.ValueObjects;
using MediatR;

namespace CarListApp.Api.Service.Queries;

public class GetCarListQuery : IRequest<List<Car>>
{

}
