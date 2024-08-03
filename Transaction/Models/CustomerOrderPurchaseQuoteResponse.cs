namespace KanzApi.Transaction.Models;

public class CustomerOrderPurchaseQuoteResponse
{

    public Guid Id { get; set; }

    public CustomerOrderStoreOrderResponse? StoreOrder { get; set; }

    public int ConfirmedQuantity { get; set; }
}
