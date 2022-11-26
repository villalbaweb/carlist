using Microsoft.EntityFrameworkCore;

public class CarListDbContext : DbContext
{
	public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
	{

	}

	public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id= 1,
                Make = "Honda",
                Model = "Fit",
                Vin = "ABC"
            },
            new Car
            {
                Id = 2,
                Make = "Honda",
                Model = "Civic",
                Vin = "ABC1"
            },
            new Car
            {
                Id = 3,
                Make = "Honda",
                Model = "Stream",
                Vin = "ABC2"
            },
            new Car
            {
                Id = 4,
                Make = "Honda",
                Model = "Star",
                Vin = "ABC3"
            },
            new Car
            {
                Id = 5,
                Make = "Chevrolet",
                Model = "Cheyyenne",
                Vin = "V1"
            },
            new Car
            {
                Id = 6,
                Make = "Ford",
                Model = "Mustang",
                Vin = "F1"
            },
            new Car
            {
                Id = 7,
                Make = "Buggatu",
                Model = "Veyron",
                Vin = "BV1"
            },
            new Car
            {
                Id = 8,
                Make = "Buggati",
                Model = "Chyron",
                Vin = "BC1"
            }
        );
    }
}