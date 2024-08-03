using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Oto.Models;

public class OtoRefreshTokenResponse : OtoResponse
{

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public string? ExpiresIn { get; set; }
}
