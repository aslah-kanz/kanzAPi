using KanzApi.Utils;

namespace KanzApi.Vendors.SendGrid.Models;

public class SendGridReceiptAdditionalItemRequest
{

    public string? Name { get; set; }

    public string? Amount { get; set; }

    public static SendGridReceiptAdditionalItemRequest From(string name, decimal amount)
    {
        return new()
        {
            Name = name,
            Amount = "SAR " + amount.ToString(Constants.DecimalFormat),
        };
    }
}
