using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class PurchaseQuoteInvoiceItemResponse
{

    public Guid Id { get; set; }

    public PurchaseQuoteInvoiceProductResponse Product { get; set; } = new();

    public string VendorSku { get; set; } = "";

    public int RequestedQuantity { get; set; }

    public int TotalRequestedQuantity { get; set; }

    public int ConfirmedQuantity { get; set; }

    public EPurchaseQuoteStatus? Status { get; set; }

    public decimal OriginalPrice { get; set; }
}
