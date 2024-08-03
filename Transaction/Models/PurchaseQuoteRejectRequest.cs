using KanzApi.Common.Validation;
using KanzApi.Resources;


namespace KanzApi.Transaction.Models;

public class PurchaseQuoteRejectRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Comment { get; set; }
}
