using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Oto.Models;

public class DeliveryHistoryResponse
{

    public string? DeliveryCompany { get; set; }

    public string? ShipmentId { get; set; }

    public string? Status { get; set; }

    public string? DriverName { get; set; }

    public string? DriverPhone { get; set; }

    public string? TrackingUrl { get; set; }

    [JsonPropertyName("Items")]
    public List<OtoHistoryResponse>? History { get; set; }
}
