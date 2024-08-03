namespace KanzApi.Transaction.Entities;

public enum ECustomerOrderStatus
{

    Open,
    WaitingApproval,
    Rejected,
    WaitingPayment,
    InPayment,
    Failed,
    Paid,
    CanceledBySystem,
    Packing,
    OnDelivery,
    Canceled,
    Completed
}
