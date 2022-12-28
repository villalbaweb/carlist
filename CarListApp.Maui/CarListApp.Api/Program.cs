using CarListApp.Api.Configuration.Endpoints;
using CarListApp.Api.Configuration.ServiceCollection;
using CarListApp.Api.Core.Settings;

var builder = WebApplication.CreateBuilder(args);

JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
ConnectionStrings connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

builder.Services
    .ConfigureSwaggerBehavior()
    .ConfigureCorsBehavior()
    .ConfigureDbBehavior(connectionStrings)
    .ConfigureAuthBehavior(jwtSettings)
    .ConfigureMediatorBehavior()
    .RegisterDependencies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.RegisterAuthEndpoints(jwtSettings);
app.RegisterCarEndpoints();

app.Run();