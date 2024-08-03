using KanzApi.Common.Validation;
using KanzApi.Product.Entities;
using KanzApi.Resources;

namespace KanzApi.Product.Models;

public class ProductStatusRequest
{

    [NotNull(ErrorMessageResourceName = "NotNull", ErrorMessageResourceType = typeof(ValidationMessages))]
    public EProductStatus? Status { get; set; }
}
