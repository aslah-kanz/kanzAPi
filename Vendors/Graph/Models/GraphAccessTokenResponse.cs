using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Graph.Models;

public class GraphAccessTokenResponse
{

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}
