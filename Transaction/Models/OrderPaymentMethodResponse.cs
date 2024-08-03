using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class OrderPaymentMethodResponse
{

    public int Id { get; set; }

    public LocalizableString Name { get; set; } = new();
}
