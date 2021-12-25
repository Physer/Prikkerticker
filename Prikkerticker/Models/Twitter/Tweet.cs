using System;
using System.Text.Json.Serialization;

namespace Prikkerticker.Models.Twitter;

public class Tweet
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    public string? Id { get; set; }
    public string? Text { get; set; }
}
