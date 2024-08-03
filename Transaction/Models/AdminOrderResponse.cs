using KanzApi.Account.Models;
using KanzApi.Product.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class AdminOrderResponse
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? InvoiceNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public AdminOrderPrincipalResponse? Principal { get; set; }

    public PrincipalAddressResponse? Address { get; set; } = new();

    public decimal? EstimatedDeliveryCost { get; set; }

    public decimal SubTotal { get; set; }

    public string? PromoCode { get; set; }

    public decimal? DiscountPrice { get; set; }

    public decimal? GrandTotal { get; set; }

    public ECustomerOrderStatus Status { get; set; }

    public ISet<CustomerOrderCustomerOrderItemResponse> Items { get; set; } = new HashSet<CustomerOrderCustomerOrderItemResponse>();
}
