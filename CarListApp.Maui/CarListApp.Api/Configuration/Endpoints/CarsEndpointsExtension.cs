using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Infrastructure;
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

        endpoints.MapPut("/cars/{id}", async (CarListDbContext db, int id, Car car) =>
        {
            var record = await db.Cars.FindAsync(id);
            if (record is null) return Results.NotFound();

            record.Model = car.Model;
            record.Make = car.Make;
            record.Vin = car.Vin;

            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        endpoints.MapDelete("/cars/{id}", async (CarListDbContext db, int id) =>
        {
            var record = await db.Cars.FindAsync(id);
            if (record is null) return Results.NotFound();

            db.Remove(record);
            await db.SaveChangesAsync();

            return Results.NoContent();
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
