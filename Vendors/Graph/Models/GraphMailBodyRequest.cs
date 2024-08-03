using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class GraphMailBodyRequest
{

    [JsonPropertyName("contentType")]
    public string? ContentType { get; set; } = "HTML";

    [JsonPropertyName("content")]
    public string? Content { get; set; }
}
