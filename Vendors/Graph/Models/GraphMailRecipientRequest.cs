using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Msegat.Models;

public class GraphMailRecipientRequest
{

    [JsonPropertyName("emailAddress")]
    public GraphMailAddressRequest? EmailAddress { get; set; }
}
