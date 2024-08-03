using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class MsegatSmsRequest : MsegatRequest
{

    [JsonPropertyName("numbers")]
    public string? Number { get; set; }

    [JsonPropertyName("msg")]
    public string? Message { get; set; }
}
