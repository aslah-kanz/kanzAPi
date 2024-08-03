using System.Text.Json.Serialization;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoDeliveryFeeRequest : IHttpClientRequest
{

    [JsonPropertyName("originCity")]
    public string? OriginCity { get; set; }

    [JsonPropertyName("destinationCity")]
    public string? DestinationCity { get; set; }

    [JsonPropertyName("originCountry")]
    public string? OriginCountry { get; set; }

    [JsonPropertyName("destinationCountry")]
    public string? DestinationCountry { get; set; }

    [JsonPropertyName("weight")]
    public double? Weight { get; set; } = 0;

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("packageCount")]
    public int? PackageCount { get; set; }

    [JsonPropertyName("totalDue")]
    public int? TotalDue { get; set; } = 0;

    [JsonPropertyName("length")]
    public double? Length { get; set; }

    [JsonPropertyName("width")]
    public double? Width { get; set; }

    [JsonPropertyName("height")]
    public double? Height { get; set; }
}
