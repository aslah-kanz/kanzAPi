using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class FaqPageableParam : PageableParam<EFaqSort, Faq>
{
    [SwaggerParameter("<i>Contains</i> : question")]
    public override string? Search { get; set; }

    public FaqPageableParam() : base(EFaqSort.UpdatedAt) { }

    protected override Expression<Func<Faq, bool>> ToSearchPredicate(string search)
    {
        return Faq.QQuestionContains(search);
    }
}
