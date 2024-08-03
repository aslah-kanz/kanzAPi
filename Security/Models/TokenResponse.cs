using System.Text.Json.Serialization;

namespace KanzApi.Security.Models;

public class TokenResponse
{

    [JsonIgnore]
    public string? Id { get; set; }

    public string Token { get; set; } = "";

    public DateTime ExpiredAt { get; set; }
}
