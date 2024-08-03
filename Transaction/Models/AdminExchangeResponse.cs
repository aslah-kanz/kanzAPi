using System.Text.Json.Serialization;
using KanzApi.Common.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class AdminExchangeResponse
{

    public Guid Id { get; set; }

    public AdminPurchaseQuoteResponse? PurchaseQuote { get; set; }

    public ExchangePrincipalResponse? Principal { get; set; }

    public string? Number { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<ImageResponse> Images { get; set; } = [];

    public ExchangeProductResponse Product { get; set; } = new();

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? SubTotal { get; set; }

    [JsonPropertyName("reason")]
    public string? Comment { get; set; }

    public string? AdminComment { get; set; }

    public string? VendorComment { get; set; }

    public EExchangeStatus? Status { get; set; }
}
