using KanzApi.Common.Validation;
using KanzApi.Resources;

namespace KanzApi.Transaction.Models.Param;

public class CartProductParam
{

    [NotEmpty(ErrorMessageResourceName = "NotEmpty", ErrorMessageResourceType = typeof(ValidationMessages))]
    public ISet<int> Ids { get; set; } = new HashSet<int>();
}
