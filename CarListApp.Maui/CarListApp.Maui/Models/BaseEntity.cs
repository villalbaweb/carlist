using SQLite;
using System.Text.Json.Serialization;

namespace CarListApp.Maui.Models;

public abstract class BaseEntity
{
    [PrimaryKey]
    [AutoIncrement]
    [JsonPropertyName("id")]
    public int Id { get; set; }
}
