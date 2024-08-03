namespace KanzApi.Vendors.Oto.Models;

public class OtoWebHookRequest
{
    public string? DcStatus { get; set; }

    public string? Note { get; set; }

    public string? BrandName { get; set; }

    public string? DeliveryOptionName { get; set; }

    public int? ShipmentWeight { get; set; }

    public string? OrderId { get; set; }

    public string? TrackingUrl { get; set; }

    public bool? ReverseShipment { get; set; }
    
    public string? DeliveryCompany { get; set; }

    public string? PrintAwbUrl { get; set; }

    public string? BrandedTrackingURL { get; set; }

    public string? TrackingNumber { get; set; }

    public string? Status { get; set; }

    public long? Timestamp { get; set; }
}
