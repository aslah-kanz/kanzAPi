using KanzApi.Transaction.Models;

namespace KanzApi.Product.Models;

public class CustomerOrderCustomerOrderItemResponse
{

    public Guid Id { get; set; }

    public CustomerOrderProductResponse Product { get; set; } = new();

    public int Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal SubTotal { get; set; }

    public string? Comment { get; set; }

    public virtual IEnumerable<CustomerOrderPurchaseQuoteResponse> PurchaseQuotes { get; set; } = [];
}
