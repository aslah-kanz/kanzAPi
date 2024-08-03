using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class PurchaseQuoteInvoiceProductResponse
{

    public string? Mpn { get; set; }

    public LocalizableString Name { get; set; } = new();

    public ImageResponse? Image { get; set; }

    public LocalizableNameResponse Brand { get; set; } = new();
}
