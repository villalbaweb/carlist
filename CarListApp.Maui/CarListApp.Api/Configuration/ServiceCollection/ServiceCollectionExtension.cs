﻿using CarListApp.Api.Configuration.Filters;
using CarListApp.Api.Core.Dtos;
using CarListApp.Api.Core.Interfaces;
using CarListApp.Api.Core.Settings;
using CarListApp.Api.DtoValidators;
using CarListApp.Api.Infrastructure;
using CarListApp.Api.Infrastructure.EmailSendProvider;
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
    internal static IServiceCollection AddSettingsConfigurationBehavior(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        return services;
    }

    internal static IServiceCollection ConfigureSwaggerBehavior(this IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
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

        return services;
    }

    internal static IServiceCollection ConfigureCorsBehavior(this IServiceCollection services)
    {
        services.AddCors(o =>
        {
            o.AddPolicy("AllowAll", a => a
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
        });

        return services;
    }

    internal static IServiceCollection ConfigureDbBehavior(this IServiceCollection services, IConfiguration configuration)
    {
        ConnectionStrings connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        var conn = new SqliteConnection(connectionStrings.SqiteConnString);

        services.AddDbContext<CarListDbContext>(o => o.UseSqlite(conn));

        return services;
    }

    internal static IServiceCollection ConfigureAuthBehavior(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSettings jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<CarListDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(10));

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

        return services;
    }

    internal static IServiceCollection ConfigureMediatorBehavior(this IServiceCollection services)
    {
        // MediatoR
        // Use typeof to identify any of the classes belonging to the assembly that contains the Commands, Queries and Handlers
        services.AddMediatR(typeof(GetCarByIdHandler));

        return services;
    }

    internal static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        // DTO Validators
        services.AddScoped<IValidator<IdentityUserDto>, IdentityUserValidator>();
        
        // Generic Dependencies
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        // Repositories
        services.AddScoped<ICarRepository, CarRepository>();

        // Providers
        services.AddScoped<IEmailSender, SmtpEmailSender > ();

        return services;
    }
}
