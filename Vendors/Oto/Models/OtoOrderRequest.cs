using System.Text.Json.Serialization;
using KanzApi.Messaging.Converters;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Oto.Models;

public class OtoOrderRequest : IHttpClientRequest
{

    [JsonPropertyName("orderId")]
    public string? OrderId { get; set; }

    [JsonPropertyName("parentOrderId")]
    public string? ParentOrderId { get; set; }

    [JsonPropertyName("ref1")]
    public string? Ref1 { get; set; }

    [JsonPropertyName("pickupLocationCode")]
    public string? PickupLocationCode { get; set; }

    [JsonPropertyName("createShipment")]
    public bool? CreateShipment { get; set; }

    [JsonPropertyName("serviceType")]
    public string? ServiceType { get; set; }

    [JsonPropertyName("forReverseShipment")]
    public bool? ForReverseShipment { get; set; }

    [JsonPropertyName("deliveryOptionId")]
    public string? DeliveryOptionId { get; set; }

    [JsonPropertyName("storeName")]
    public string? StoreName { get; set; }

    [JsonPropertyName("paymentMethod")]
    public string? PaymentMethod { get; set; }

    [JsonPropertyName("amount")]
    public double? Amount { get; set; }

    [JsonPropertyName("amount_due")]
    public double? AmountDue { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("shippingAmount")]
    public double? ShippingAmount { get; set; }

    [JsonPropertyName("subtotal")]
    public double? Subtotal { get; set; }

    [JsonPropertyName("shippingNotes")]
    public string? ShippingNotes { get; set; }

    [JsonPropertyName("packageSize")]
    public string? PackageSize { get; set; }

    [JsonPropertyName("packageCount")]
    public int? PackageCount { get; set; }

    [JsonPropertyName("packageWeight")]
    public double? PackageWeight { get; set; }

    [JsonPropertyName("boxWidth")]
    public double? BoxWidth { get; set; }

    [JsonPropertyName("boxLength")]
    public double? BoxLength { get; set; }

    [JsonPropertyName("boxHeight")]
    public double? BoxHeight { get; set; }

    [JsonPropertyName("deliverySlotDate")]
    [JsonConverter(typeof(OtoDateTimeConverter))]
    public DateTime? DeliverySlotDate { get; set; }

    [JsonPropertyName("deliverySlotFrom")]
    public string? DeliverySlotFrom { get; set; }

    [JsonPropertyName("deliverySlotTo")]
    public string? DeliverySlotTo { get; set; }

    [JsonPropertyName("orderDate")]
    [JsonConverter(typeof(OtoDateTimeConverter))]
    public DateTime? OrderDate { get; set; }

    [JsonPropertyName("senderName")]
    public string? SenderName { get; set; }

    [JsonPropertyName("customer")]
    public OtoCustomerRequest? Customer { get; set; }

    [JsonPropertyName("items")]
    public List<OtoItemRequest>? Items { get; set; } = [];
}
