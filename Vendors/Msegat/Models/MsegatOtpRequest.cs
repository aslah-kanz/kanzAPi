using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class MsegatOtpRequest : MsegatRequest
{

    [JsonPropertyName("number")]
    public string? Number { get; set; }
}
