﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarListApp.Api.Migrations
{
    [DbContext(typeof(CarListDbContext))]
    partial class CarListDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Make = "Honda",
                            Model = "Fit",
                            Vin = "ABC"
                        },
                        new
                        {
                            Id = 2,
                            Make = "Honda",
                            Model = "Civic",
                            Vin = "ABC1"
                        },
                        new
                        {
                            Id = 3,
                            Make = "Honda",
                            Model = "Stream",
                            Vin = "ABC2"
                        },
                        new
                        {
                            Id = 4,
                            Make = "Honda",
                            Model = "Star",
                            Vin = "ABC3"
                        },
                        new
                        {
                            Id = 5,
                            Make = "Chevrolet",
                            Model = "Cheyyenne",
                            Vin = "V1"
                        },
                        new
                        {
                            Id = 6,
                            Make = "Ford",
                            Model = "Mustang",
                            Vin = "F1"
                        },
                        new
                        {
                            Id = 7,
                            Make = "Buggatu",
                            Model = "Veyron",
                            Vin = "BV1"
                        },
                        new
                        {
                            Id = 8,
                            Make = "Buggati",
                            Model = "Chyron",
                            Vin = "BC1"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
