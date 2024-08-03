using KanzApi.Common.Models;
using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class PaymentMethodRequest
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Name { get; set; }

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public LocalizableString? Instruction { get; set; }
}
