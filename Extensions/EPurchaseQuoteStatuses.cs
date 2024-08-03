using KanzApi.Transaction.Entities;

namespace KanzApi.Extensions;

public static class EPurchaseQuoteStatuses
{

    public static bool IsAvailable(this EPurchaseQuoteStatus source)
    {
        switch (source)
        {
            case EPurchaseQuoteStatus.WaitingPayment:
            case EPurchaseQuoteStatus.Open:
            case EPurchaseQuoteStatus.Accepted:
            case EPurchaseQuoteStatus.ReadyForPacking:
            case EPurchaseQuoteStatus.ReadyForDelivery:
            case EPurchaseQuoteStatus.AssignedToWarehouse:
            case EPurchaseQuoteStatus.ShipmentCreated:
            case EPurchaseQuoteStatus.SearchingDriver:
            case EPurchaseQuoteStatus.GoingToPickup:
            case EPurchaseQuoteStatus.PickedUp:
            case EPurchaseQuoteStatus.ArrivedOriginTerminal:
            case EPurchaseQuoteStatus.OutForDelivery:
            case EPurchaseQuoteStatus.ArrivedDestination:
            case EPurchaseQuoteStatus.Delivered:
                return true;
        }
        return false;
    }

    public static List<EPurchaseQuoteStatus> Unavailables()
    {
        List<EPurchaseQuoteStatus> list = [];
        foreach (EPurchaseQuoteStatus value in Enum.GetValues(typeof(EPurchaseQuoteStatus)))
        {
            if (!value.IsAvailable()) list.Add(value);
        }
        return list;
    }
}
