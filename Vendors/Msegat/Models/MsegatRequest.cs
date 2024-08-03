using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Msegat.Models;

public class MsegatRequest : IHttpClientRequest
{

    [JsonPropertyName("userName")]
    public string? Username { get; set; }

    [JsonPropertyName("apiKey")]
    public string? ApiKey { get; set; }

    [JsonPropertyName("userSender")]
    public string? UserSender { get; set; }
}
