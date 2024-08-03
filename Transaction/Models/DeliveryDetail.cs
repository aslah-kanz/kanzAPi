namespace KanzApi.Transaction.Models;

public class DeliveryDetail
{

    public int? StoreId { get; set; }

    public string? OriginCity { get; set; }

    public string? DestinationCity { get; set; }

    public string? OriginCountry { get; set; }

    public string? DestinationCountry { get; set; }

    public double? Weight { get; set; } = 0;

    public double? Volume { get; set; } = 0;
}
