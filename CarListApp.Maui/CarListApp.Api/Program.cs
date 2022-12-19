using CarListApp.Api.Configuration.Endpoints;
using CarListApp.Api.Configuration.ServiceCollection;
using CarListApp.Api.Core.Settings;

var builder = WebApplication.CreateBuilder(args);

JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.ConfigureSwaggerBehavior();
builder.Services.ConfigureDbBehavior();
builder.Services.ConfigureAuthBehavior(jwtSettings);

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