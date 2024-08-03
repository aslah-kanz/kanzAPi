using System.Text.Json.Serialization;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class PurchaseQuoteInvoiceResponse
{

    public string InvoiceNumber { get; set; } = "";

    public string StoreName { get; set; } = "";

    public decimal NetAmount { get; set; } = 0;

    public decimal PlatformCommission { get; set; } = 0;

    public decimal TotalAmount { get; set; } = 0;

    [JsonPropertyName("status")]
    public EPurchaseQuoteStatus PurchaseQuoteStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<PurchaseQuoteInvoiceItemResponse> Items { get; set; } = [];
}
