using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class PurchaseQuoteInvoiceItem
{

    public string? InvoiceNumber { get; set; }

    public int ProductCount { get; set; }

    public string? StoreName { get; set; }

    public decimal? Total { get; set; }
    public decimal? Profit { get; set; }

    public EPurchaseQuoteStatus? Status { get; set; }

    public DateTime? CreatedAt { get; set; }
}
