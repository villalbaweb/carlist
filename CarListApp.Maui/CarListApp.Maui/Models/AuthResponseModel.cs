using System.Text.Json.Serialization;

namespace CarListApp.Maui.Models;

public class AuthResponseModel
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }
}