using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class AdminOrderItem
{

    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? InvoiceNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public AdminOrderPrincipalResponse? Principal { get; set; }

    public decimal? GrandTotal { get; set; }

    public ECustomerOrderStatus Status { get; set; }

    public IList<AdminPurchaseQuoteResponse?> PurchaseQuotes { get; set; } = [];
}
