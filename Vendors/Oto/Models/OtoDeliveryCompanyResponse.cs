using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Oto.Models;

public class OtoDeliveryCompanyResponse
{

    public string? ServiceType { get; set; }

    public string? DeliveryOptionName { get; set; }

    public string? TrackingType { get; set; }

    public decimal? CodCharge { get; set; }

    public string? PickupCutOffTime { get; set; }

    public decimal? MaxOrderValue { get; set; }

    public string? InsurancePolicy { get; set; }

    [JsonPropertyName("maxCODValue")]
    public decimal? MaxCodValue { get; set; }

    public int? DeliveryOptionId { get; set; }

    public decimal? ExtraWeightPerKg { get; set; }

    public string? DeliveryCompanyName { get; set; }

    public decimal? ReturnFee { get; set; }

    public decimal? MaxFreeWeight { get; set; }

    [JsonPropertyName("avgDeliveryTime")]
    public string? AverageDeliveryTime { get; set; }

    public decimal? Price { get; set; }

    public string? Logo { get; set; }

    public string? PickupDropoff { get; set; }

    [JsonIgnore]
    public int MinEstimatedDay { get; set; }
}
