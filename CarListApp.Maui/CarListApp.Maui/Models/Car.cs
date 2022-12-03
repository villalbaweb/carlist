using SQLite;
using System.Text.Json.Serialization;

namespace CarListApp.Maui.Models;

[Table("car")]
public class Car : BaseEntity
{
    [JsonPropertyName("make")]
    public string Make { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [MaxLength(12)]
    [Unique]
    [JsonPropertyName("vin")]
    public string Vin { get; set; }
}
