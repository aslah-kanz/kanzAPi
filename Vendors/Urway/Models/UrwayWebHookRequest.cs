using KanzApi.Messaging.Converters;
using KanzApi.Messaging.Models;
using KanzApi.Vendors.Urway.Validation;
using System.Text.Json.Serialization;

namespace KanzApi.Vendors.Urway.Models;

[UrwayResponseHash]
public class UrwayWebHookRequest : IHttpClientResponse, IUrwayValidatableResponse
{

    public string? ResponseCode { get; set; }

    public string? Amount { get; set; }

    public string? AuthCode { get; set; }

    [JsonPropertyName("maskedPAN")]
    public string? MaskedPan { get; set; }

    public string? PaymentId { get; set; }

    [JsonPropertyName("ECI")]
    public string? Eci { get; set; }

    public string? TerminalId { get; set; }

    public string? ResponseHash { get; set; }

    public string? PayFor { get; set; }

    [JsonPropertyName("TranId")]
    public string? TransactionId { get; set; }

    public string? CardToken { get; set; }

    public string? Result { get; set; }

    [JsonPropertyName("RRN")]
    public string? Rrn { get; set; }

    [JsonConverter(typeof(UrwayMetadataModelBase64Converter))]
    public UrwayMetadataModel? MetaData { get; set; }

    public string? SubscriptionId { get; set; }

    public string? PaymentType { get; set; }

    public string? CardBrand { get; set; }

    public string? Event { get; set; }

    public string? Email { get; set; }

    public string? TrackId { get; set; }

    public string? UserField1 { get; set; }

    public string? UserField3 { get; set; }

    public string? UserField4 { get; set; }

    public string? UserField5 { get; set; }

    public bool IsSuccess()
    {
        return (ResponseCode == null || ResponseCode.Equals("000"))
        && Event!.Equals("Transaction.Success");
    }
}
