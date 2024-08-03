using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.TinyUrl.Models;

public class TinyUrlCreateRequest : IHttpClientRequest
{

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    [JsonPropertyName("tags")]
    public string? Tags { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
