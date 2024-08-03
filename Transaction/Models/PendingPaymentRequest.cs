namespace KanzApi.Transaction.Models;

public record PendingPaymentRequest
{
    public List<PendingPaymentItem>? CustomerOrders { get; set; }
}

public record PendingPaymentItem
{
    public Guid CustomerOrderId { get; set; }
    public int CustomerPrincipalId { get; set; }
}
