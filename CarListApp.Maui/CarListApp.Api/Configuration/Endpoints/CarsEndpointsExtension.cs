using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Service.Commands;
using CarListApp.Api.Service.Queries;
using MediatR;

namespace CarListApp.Api.Configuration.Endpoints;

internal static class CarsEndpointsExtension
{
    internal static void RegisterCarEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/cars", async (CancellationToken token, IMediator _mediator) => await _mediator.Send(new GetCarListQuery(), token));

        endpoints.MapGet("/cars/{id}", async (CancellationToken token, IMediator _mediator, int id) =>
        {
            var car = await _mediator.Send(new GetCarByIdQuery
            {
                Id = id
            }, token);

            return car is not null ? Results.Ok(car) : Results.NotFound();
        });

        endpoints.MapPut("/cars/{id}", async (CancellationToken token, IMediator _mediator, int id, Car car) => await _mediator.Send(new UpdateCarCommand(id, car.Make, car.Model, car.Vin), token)
                ? Results.NoContent()
                : Results.NotFound());

        endpoints.MapDelete("/cars/{id}", async (CancellationToken token, IMediator _mediator, int id) => await _mediator.Send(new DeleteCarCommand { Id = id }, token) 
                ? Results.NoContent() 
                : Results.NotFound());

        endpoints.MapPost("/cars", async (CancellationToken token, IMediator _mediator, Car car) =>
        {
            Car newCar = await _mediator.Send(new CreateCarCommand(
                car.Make,
                car.Model,
                car.Vin), token);

            return Results.Created($"/cars/{newCar.Id}", newCar);
        });
    }
}
