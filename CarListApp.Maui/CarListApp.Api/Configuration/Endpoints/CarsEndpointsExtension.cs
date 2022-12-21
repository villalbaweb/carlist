using CarListApp.Api.Core.ValueObjects;
using CarListApp.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarListApp.Api.Configuration.Endpoints;

internal static class CarsEndpointsExtension
{
    internal static void RegisterCarEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());

        endpoints.MapGet("/cars/{id}", async (CarListDbContext db, int id) => await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound());

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

        endpoints.MapPost("/cars", async (CarListDbContext db, Car car) =>
        {
            await db.AddAsync(car);
            await db.SaveChangesAsync();

            return Results.Created($"/cars/{car.Id}", car);
        });
    }
}
