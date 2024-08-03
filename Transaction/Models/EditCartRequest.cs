using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models;

public class EditCartRequest
{

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public int Quantity { get; set; } = 0;

    [MaxSize(255, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ValidationMessages))]
    public string? Comment { get; set; }
}
