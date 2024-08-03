using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class PurchaseQuoteByStoreOrderResponse
{
    public Guid Id { get; set; }

    public int ConfirmedQuantity { get; set; }

    public bool IsReviewed { get; set; }

    public bool IsRefundable { get; set; }

    public bool IsExchangeable { get; set; }

    public bool IsExchanged { get; set; }

    public bool IsRefunded { get; set; }

    public EPurchaseQuoteStatus? Status { get; set; }

    public PurchaseQuoteProductResponse? Product { get; set; }

    public CustomerOrderStoreOrderResponse? StoreOrder { get; set; }
    
    public PurchaseQuoteCustomerOrderItemResponse? CustomerOrderItem { get; set; }
}
