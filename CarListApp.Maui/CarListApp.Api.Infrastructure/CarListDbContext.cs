using CarListApp.Api.Core.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarListApp.Api.Infrastructure;

public class CarListDbContext : IdentityDbContext
{
	public CarListDbContext(DbContextOptions<CarListDbContext> options) : base(options)
	{
        Database.EnsureCreated();
	}

	public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>().HasData(
            new Car
            {
                Id = 1,
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

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "66dec4b4-76b9-4d2f-abf9-429038afe3aa",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "45d8f472-a7b5-46df-b585-877b7fed6853",
                Name = "User",
                NormalizedName = "USER"
            }
        );

        var hasher = new PasswordHasher<IdentityUser>();

        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "56c168b6-904f-479e-a580-8949b3c394cc",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true
            },
            new IdentityUser
            {
                Id = "386ec0df-f833-4091-a145-1fee28253603",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                EmailConfirmed = true
            }
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "66dec4b4-76b9-4d2f-abf9-429038afe3aa",
                UserId = "56c168b6-904f-479e-a580-8949b3c394cc"
            },
            new IdentityUserRole<string>
            {
                RoleId = "45d8f472-a7b5-46df-b585-877b7fed6853",
                UserId = "386ec0df-f833-4091-a145-1fee28253603"
            }
        );
    }
}