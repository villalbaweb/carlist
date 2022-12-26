using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Service.Commands;
using CarListApp.Api.Service.Queries;
using MediatR;

namespace CarListApp.Api.Configuration.Endpoints;

internal static class CarsEndpointsExtension
{
    internal static void RegisterCarEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/cars", async (IMediator _mediator) => await _mediator.Send(new GetCarListQuery()));

        endpoints.MapGet("/cars/{id}", async (IMediator _mediator, int id) =>
        {
            var car = await _mediator.Send(new GetCarByIdQuery
            {
                Id = id
            });

            return car is not null ? Results.Ok(car) : Results.NotFound();
        });

        endpoints.MapPut("/cars/{id}", async (IMediator _mediator, int id, Car car) =>
        {
            return await _mediator.Send(new UpdateCarCommand(id, car.Make, car.Model, car.Vin))
                ? Results.NoContent()
                : Results.NotFound();
        });

        endpoints.MapDelete("/cars/{id}", async (IMediator _mediator, int id) =>
        {
            return await _mediator.Send(new DeleteCarCommand { Id = id }) 
                ? Results.NoContent() 
                : Results.NotFound();
        });

        endpoints.MapPost("/cars", async (IMediator _mediator, Car car) =>
        {
            Car newCar = await _mediator.Send(new CreateCarCommand(
                car.Make,
                car.Model,
                car.Vin));

            return Results.Created($"/cars/{newCar.Id}", newCar);
        });
    }
}
