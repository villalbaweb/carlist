using CarListApp.Api.Configuration.Endpoints;
using CarListApp.Api.Configuration.ServiceCollection;

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

internal record LoginDto(string Username, string Password);
internal record AuthResponseDto(string UserId, string Username, string Token);
public record JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int DurationInMinutes { get; set; }
}