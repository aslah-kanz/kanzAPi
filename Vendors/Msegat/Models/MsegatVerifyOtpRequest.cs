using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class MsegatVerifyOtpRequest : MsegatRequest
{

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("id")]
    public int? Id { get; set; }
}
