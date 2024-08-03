using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoErrorWrapperResponse : IHttpClientResponse
{

    public int? Code { get; set; }

    public string? Message { get; set; }

    public int? ErrorCode { get; set; }

    [JsonPropertyName("errorMsg")]
    public string? ErrorMessage { get; set; }

    public OtoErrorResponse? Error { get; set; }

    public bool IsSuccess()
    {
        throw new NotImplementedException();
    }
}
