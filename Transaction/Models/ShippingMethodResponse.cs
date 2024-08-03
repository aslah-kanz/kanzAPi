using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Transaction.Models;

public class ShippingMethodResponse
{

    public int Id { get; set; }

    public string? Code { get; set; }

    public string ProviderName { get; set; } = "";

    public string DeliveryCompanyName { get; set; } = "";

    public string DeliveryEstimateTime { get; set; } = "";

    public OtoDeliveryFeeResponse? Detail { get; set; }
}
