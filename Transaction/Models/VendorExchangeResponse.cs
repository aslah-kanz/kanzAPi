using System.Text.Json.Serialization;
using KanzApi.Common.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class VendorExchangeResponse
{

    public Guid Id { get; set; }

    [JsonPropertyName("exchangeNumber")]
    public string? Number { get; set; }

    public decimal? Total { get; set; }

    [JsonPropertyName("reason")]
    public string? Comment { get; set; }

    public List<ImageResponse> Images { get; set; } = [];

    public EExchangeStatus? Status { get; set; }
}
