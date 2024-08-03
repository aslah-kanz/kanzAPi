using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class VendorExchangeRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? VendorComment { get; set; }
}
