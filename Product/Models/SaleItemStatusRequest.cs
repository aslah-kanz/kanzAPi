using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Product.Models;

public class SaleItemStatusRequest
{

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public bool? Enabled { get; set; }
}
