namespace KanzApi.Transaction.Models;

public class PurchaseQuoteCustomerOrderItemResponse
{
    public Guid? Id { get; set; }
    
    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? SubTotal { get; set; }

    public string? Comment { get; set; }
}