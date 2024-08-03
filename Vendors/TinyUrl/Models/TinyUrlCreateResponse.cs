using System.Text.Json.Serialization;

namespace KanzApi.Vendors.TinyUrl.Models;

public class TinyUrlCreateResponse
{
    public string? Domain { get; set; }

    public string? Alias { get; set; }

    public bool? Deleted { get; set; }

    public bool? Archived { get; set; }

    public List<string>? Tags { get; set; }

    [JsonPropertyName("analytics")]
    public TinyUrlAnalyticResponse? Analytic { get; set; }

    [JsonPropertyName("tiny_url")]
    public string? TinyUrl { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    public string? Url { get; set; }
}
