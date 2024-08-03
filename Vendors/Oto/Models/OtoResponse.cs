using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoResponse : IHttpClientResponse
{

    public bool Success { get; set; }

    public int? ErrorCode { get; set; }

    [JsonPropertyName("errorMsg")]
    public string? ErrorMessage { get; set; }

    public bool IsSuccess()
    {
        return Success;
    }
}
