namespace KanzApi.Utils;

public class DateUtils
{
    /// <summary>
    /// Requirement: https://trello.com/c/F1Oz90vK/53-cus-33-a-sync-information-in-my-order-page
    /// Will set the purchase quote is exchangeable or refundable based on purchase quote's updated date
    /// Based on the requirement user is allow to exchange or refund until 15 days after purchase quote is delivered
    /// </summary>
    /// <param name="updatedDate"></param>
    /// <returns>User is allow to exchange or refund</returns>
    public static bool SetIsExchangeableIsRefundable(DateTime? updatedDate)
    {
        if (updatedDate == null) return false;
        var lastExchangeableRefundableDate = updatedDate.Value.AddDays(15);

        var diff = lastExchangeableRefundableDate - DateTime.Now;
        return diff.Days > 0;
    }
}