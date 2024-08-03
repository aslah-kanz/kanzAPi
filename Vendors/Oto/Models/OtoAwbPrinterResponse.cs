using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoAwbPrinterResponse : IHttpClientResponse
{

    [JsonPropertyName("printAWBURL")]
    public string? PrintAwbUrl { get; set; }

    [JsonPropertyName("deliveryCompany")]
    public string? DeliveryCompany { get; set; }

    [JsonPropertyName("trackingNumber")]
    public string? TrackingNumber { get; set; }

    public bool IsSuccess()
    {
        return true;
    }
}
