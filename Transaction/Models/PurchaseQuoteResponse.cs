using KanzApi.Account.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class PurchaseQuoteResponse
{

    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? VendorSku { get; set; }

    public CustomerOrderInvoiceResponse? CustomerOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal? Price { get; set; }

    public decimal? PlatformCommission { get; set; }

    public int RequestedQuantity { get; set; }

    public int ConfirmedQuantity { get; set; }

    public int TotalRequestedQuantity { get; set; }

    public decimal? SubTotal { get; set; }

    public VendorStoreResponse? Store { get; set; }

    public PurchaseQuoteProductResponse? Product { get; set; }

    public EPurchaseQuoteStatus? Status { get; set; }

    public string? Comment { get; set; }
}
