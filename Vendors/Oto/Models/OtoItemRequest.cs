using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Oto.Models;

public class OtoItemRequest
{

    [JsonPropertyName("productId")]
    public long? ProductId { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("price")]
    public double? Price { get; set; }

    [JsonPropertyName("rowTotal")]
    public double? RowTotal { get; set; }

    [JsonPropertyName("taxAmount")]
    public double? TaxAmount { get; set; }

    [JsonPropertyName("quantity")]
    public double? Quantity { get; set; }

    [JsonPropertyName("serialNumber")]
    public string? SerialNumber { get; set; }

    [JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
