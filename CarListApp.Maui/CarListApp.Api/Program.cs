using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", a => a
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

var dbPath = Path.Join(Directory.GetCurrentDirectory(), "carlist.db");
var conn = new SqliteConnection($"Data Source={dbPath}");
builder.Services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapGet("/cars", async (CarListDbContext db) => await db.Cars.ToListAsync());
app.MapGet("/cars/{id}", async (CarListDbContext db, int id) => await db.Cars.FindAsync(id) is Car car ? Results.Ok(car) : Results.NotFound());

app.MapPut("/cars/{id}", async (CarListDbContext db, int id, Car car) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record is null) return Results.NotFound();

    record.Model = car.Model;
    record.Make = car.Make;
    record.Vin = car.Vin;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/cars/{id}", async (CarListDbContext db, int id) =>
{
    var record = await db.Cars.FindAsync(id);
    if (record is null) return Results.NotFound();

    db.Remove(record);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("/cars", async (CarListDbContext db, Car car) =>
{
    await db.AddAsync(car);
    await db.SaveChangesAsync();
    
    return Results.Created($"/cars/{car.Id}", car);
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}