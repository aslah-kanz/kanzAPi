using System.Text.Json.Serialization;
using KanzApi.Messaging.Converters;
using KanzApi.Vendors.Urway.Validation;

namespace KanzApi.Vendors.Urway.Models;

[UrwayResponseHash]
public class UrwayTransactionResponse : UrwayResponse, IUrwayValidatableResponse
{

    [JsonPropertyName("authcode")]
    public string? AuthCode { get; set; }

    [JsonPropertyName("tranid")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("trackid")]
    public string? TrackId { get; set; }

    [JsonPropertyName("terminalid")]
    public string? TerminalId { get; set; }

    [JsonPropertyName("udf1")]
    public string? Udf1 { get; set; }

    [JsonPropertyName("udf2")]
    public string? Udf2 { get; set; }

    [JsonPropertyName("udf3")]
    public string? Udf3 { get; set; }

    [JsonPropertyName("udf4")]
    public string? Udf4 { get; set; }

    [JsonPropertyName("udf5")]
    public string? Udf5 { get; set; }

    [JsonPropertyName("rrn")]
    public string? Rrn { get; set; }

    [JsonPropertyName("eci")]
    public string? Eci { get; set; }

    [JsonPropertyName("subscriptionId")]
    public string? SubscriptionId { get; set; }

    [JsonPropertyName("trandate")]
    public string? TransactionDate { get; set; }

    [JsonPropertyName("tranType")]
    public string? TransactionType { get; set; }

    [JsonPropertyName("integrationModule")]
    public string? IntegrationModule { get; set; }

    [JsonPropertyName("integrationData")]
    public string? IntegrationData { get; set; }

    [JsonPropertyName("payId")]
    public string? PaymentId { get; set; }

    [JsonPropertyName("targetUrl")]
    public string? TargetUrl { get; set; }

    [JsonPropertyName("postData")]
    public string? PostData { get; set; }

    [JsonPropertyName("intUrl")]
    public string? IntUrl { get; set; }

    [JsonPropertyName("responseHash")]
    public string? ResponseHash { get; set; }

    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("cardBrand")]
    public string? CardBrand { get; set; }

    [JsonPropertyName("maskedPAN")]
    public string? MaskedPan { get; set; }

    [JsonPropertyName("linkBasedUrl")]
    public string? LinkBasedUrl { get; set; }

    [JsonPropertyName("sadadNumber")]
    public string? SadadNumber { get; set; }

    [JsonPropertyName("billNumber")]
    public string? BillNumber { get; set; }

    [JsonPropertyName("paymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("metaData")]
    [JsonConverter(typeof(UrwayMetadataModelConverter))]
    public UrwayMetadataModel? MetaData { get; set; }

    [JsonPropertyName("cardToken")]
    public string? CardToken { get; set; }

    [JsonIgnore]
    public string RedirectUrl { get { return TargetUrl + "?paymentid=" + PaymentId; } }
}
