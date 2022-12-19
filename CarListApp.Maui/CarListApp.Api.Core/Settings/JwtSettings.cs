namespace CarListApp.Api.Core.Settings;

public record JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
    public int DurationInMinutes { get; set; }
}