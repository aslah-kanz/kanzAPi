using System.Text.Json.Serialization;
using KanzApi.Messaging.Converters;
using KanzApi.Messaging.Models;

namespace KanzApi.Vendors.Urway.Models;

public class UrwayTransactionRequest : IHttpClientRequest
{

    [JsonPropertyName("trackid")]
    public string? TrackId { get; set; }

    [JsonPropertyName("terminalId")]
    public string? TerminalId { get; set; }

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("customerEmail")]
    public string? CustomerEmail { get; set; }

    [JsonPropertyName("merchantIp")]
    public string? MerchantIp { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("amount")]
    public string? Amount { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

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

    [JsonPropertyName("metaData")]
    [JsonConverter(typeof(UrwayMetadataModelConverter))]
    public UrwayMetadataModel? MetaData { get; set; }

    [JsonPropertyName("requestHash")]
    public string? RequestHash { get; set; }
}
