namespace KanzApi.Transaction.Entities;

public enum EPurchaseQuoteStatus
{

    Unassigned,
    WaitingPayment,
    Failed,
    Open,
    Canceled,
    Rejected,
    CanceledByRejection,
    CanceledByAcceptance,
    Accepted,
    ReadyForPacking,
    ReadyForDelivery,
    CanceledBySystem,
    AssignedToWarehouse,
    ShipmentCanceled,
    ShipmentCreated,
    SearchingDriver,
    GoingToPickup,
    PickedUp,
    ArrivedOriginTerminal,
    OutForDelivery,
    UndeliveredAttempt,
    ArrivedDestination,
    Delivered,
    PickupRequest,
    WaitingForPickup,
}
