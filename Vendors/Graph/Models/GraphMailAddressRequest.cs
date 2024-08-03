using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class GraphMailAddressRequest
{

    [JsonPropertyName("address")]
    public string? Address { get; set; }
}
