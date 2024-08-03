using KanzApi.Product.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class CustomerOrderItem
{
    public Guid Id { get; set; }

    public string? InvoiceNumber { get; set; }

    public OrderPaymentMethodResponse? PaymentMethod;

    public decimal? EstimatedDeliveryCost { get; set; }

    public decimal SubTotal { get; set; }

    public string? PromoCode { get; set; }

    public decimal? DiscountPrice { get; set; }

    public decimal? GrandTotal { get; set; }

    public bool IsReviewed { get; set; }

    public ECustomerOrderStatus Status { get; set; }

    public CustomerOrderItemProductResponse HiglightedProduct { get; set; } = new();

    public DateTime CreatedAt { get; set; }

    public double? ExpireTimeLeft { get; set; }
}
