namespace KanzApi.Vendors.Oto.Models;

public class OtoOrderHistoryItemResponse
{

    public string? OrderId { get; set; }

    public string? BranchName { get; set; }

    public string? WarehouseName { get; set; }

    public string? DeliveryCompany { get; set; }

    public string? ShipmentId { get; set; }

    public string? Status { get; set; }

    public string? DriverName { get; set; }

    public string? DriverPhone { get; set; }

    public string? TrackingUrl { get; set; }

    public List<OtoHistoryResponse>? History { get; set; }
}
