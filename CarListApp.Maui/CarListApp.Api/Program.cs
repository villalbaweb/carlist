using CarListApp.Api.Configuration.Endpoints;
using CarListApp.Api.Configuration.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSettingsConfigurationBehavior(builder.Configuration)
    .ConfigureSwaggerBehavior()
    .ConfigureCorsBehavior()
    .ConfigureDbBehavior(builder.Configuration)
    .ConfigureAuthBehavior(builder.Configuration)
    .ConfigureMediatorBehavior()
    .RegisterDependencies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.RegisterAuthEndpoints(builder.Configuration);
app.RegisterCarEndpoints();

app.Run();