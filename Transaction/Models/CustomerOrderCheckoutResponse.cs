using KanzApi.Account.Models;
using KanzApi.Product.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class CustomerOrderCheckoutResponse
{
    public Guid Id { get; set; }

    public string? InvoiceNumber { get; set; }

    public PrincipalAddressResponse? Address { get; set; } = new();

    public OrderPaymentMethodResponse? PaymentMethod;

    public decimal? EstimatedDeliveryCost { get; set; }

    public decimal SubTotal { get; set; }

    public string? PromoCode { get; set; }

    public decimal? DiscountPrice { get; set; }

    public decimal? GrandTotal { get; set; }

    public ECustomerOrderStatus Status { get; set; }

    public ISet<CustomerOrderCustomerOrderItemResponse> Items { get; set; } = new HashSet<CustomerOrderCustomerOrderItemResponse>();

    public DateTime CreatedAt { get; set; }

    public bool IsCartChanges { get; set; }

    public List<DeliveryOptionResponse> DeliveryOptions { get; set; } = [];
}
