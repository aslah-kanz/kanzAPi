using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Urway.Models;

public class UrwayMetadataModel
{

    [JsonPropertyName("token")]
    public string? Token { get; set; }
}
