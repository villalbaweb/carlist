﻿using CarListApp.Api.Configuration.Filters;
using CarListApp.Api.Core.Dtos;
using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.Settings;
using CarListApp.Api.DtoValidators;
using CarListApp.Api.Infrastructure;
using CarListApp.Api.Infrastructure.Repositories;
using CarListApp.Api.Service.Handlers;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CarListApp.Api.Configuration.ServiceCollection;

internal static class ServiceCollectionExtension
{
    internal static void ConfigureSwaggerBehavior(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            //c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

            // this is added to use bearer authentication
            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer"
            });
            c.OperationFilter<AuthenticationRequirementsOperationFilter>();
        });
        services.AddCors(o =>
        {
            o.AddPolicy("AllowAll", a => a
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
        });
    }

    internal static void ConfigureDbBehavior(this IServiceCollection services, ConnectionStrings connectionStrings)
    {
        var conn = new SqliteConnection(connectionStrings.SqiteConnString);

        services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));
    }

    internal static void ConfigureAuthBehavior(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CarListDbContext>();

        services.AddAuthentication(oprions =>
        {
            oprions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            oprions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        });
    }

    internal static void RegisterDependencies(this IServiceCollection services)
    {
        // MediatoR
        // Use typeof to identify any of the classes belonging to the assembly that contains the Commands, Queries and Handlers
        services.AddMediatR(typeof(GetCarByIdHandler));

        // DTO Validators
        services.AddScoped<IValidator<IdentityUserDto>, IdentityUserValidator>();
        
        // Generic Dependencies
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Repositories
        services.AddScoped<ICarRepository, CarRepository>();
    }
}
