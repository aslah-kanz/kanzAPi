using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Urway.Models;

public class UrwayResponse : IHttpClientResponse
{

    [JsonPropertyName("result")]
    public string? Result { get; set; }

    [JsonPropertyName("responseCode")]
    public string? ResponseCode { get; set; }

    public bool IsSuccess()
    {
        return ResponseCode == null || ResponseCode.Equals("000");
    }
}
