using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoRefreshTokenRequest : IHttpClientRequest
{
    
    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }
}
