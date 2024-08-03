using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class AdminExchangeRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? AdminComment { get; set; }
}
