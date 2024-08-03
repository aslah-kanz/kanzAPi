using KanzApi.Account.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class StoreOrderGroupResponse
{
    public Guid Id { get; set; }

    public string? InvoiceNumber { get; set; }

    public PrincipalAddressResponse? Address { get; set; } = new();

    public OrderPaymentMethodResponse? PaymentMethod { get; set; } = new();

    public decimal? EstimatedDeliveryCost { get; set; }

    public decimal SubTotal { get; set; }

    public string? PromoCode { get; set; }

    public decimal? DiscountPrice { get; set; }

    public decimal? GrandTotal { get; set; }

    public ECustomerOrderStatus Status { get; set; }

    public IEnumerable<PurchaseQuoteByStoreOrderResponse> PurchaseQuotes { get; set; } = [];

    public DateTime CreatedAt { get; set; }

    public bool Cancelable { get; set; }
}
