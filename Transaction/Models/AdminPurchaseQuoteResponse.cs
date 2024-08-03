using System.Text.Json.Serialization;
using KanzApi.Account.Models;

namespace KanzApi.Transaction.Models;

public class AdminPurchaseQuoteResponse
{

    public Guid Id { get; set; }

    public string? InvoiceNumber { get { return _storeOrder!.InvoiceNumber; } }

    private PurchaseQuoteStoreOrderResponse? _storeOrder;

    [JsonIgnore]
    public PurchaseQuoteStoreOrderResponse? StoreOrder
    { get { return _storeOrder; } set { _storeOrder = value; } }

    public string? VendorSku { get; set; }

    public StoreResponse? Store { get; set; }

    public string? CustomerOrderInvoiceNumber { get { return _customerOrder!.InvoiceNumber; } }

    private PurchaseQuoteCustomerOrderResponse? _customerOrder;

    [JsonIgnore]
    public PurchaseQuoteCustomerOrderResponse? CustomerOrder
    { get { return _customerOrder; } set { _customerOrder = value; } }
}
