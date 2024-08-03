using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Oto.Models;

public class OtoDeliveryFeeResponse : OtoResponse
{

    public string? TraceId { get; set; }

    [JsonPropertyName("deliveryCompany")]
    public IEnumerable<OtoDeliveryCompanyResponse> DeliveryCompanies { get; set; } = [];
}
