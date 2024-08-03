using KanzApi.Messaging.Models;
using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Oto.Models;

public class OtoOrderHistoryRequest : IHttpClientRequest
{

    [JsonPropertyName("orderIds")]
    public List<string>? OrderIds { get; set; }
}
