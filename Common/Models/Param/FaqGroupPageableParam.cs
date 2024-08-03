using KanzApi.Common.Entities;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Common.Models.Param;

public class FaqGroupPageableParam : PageableParam<EFaqGroupSort, FaqGroup>
{

    private bool? _showAtHomePage;

    public bool? ShowAtHomePage { get { return _showAtHomePage; } set { _showAtHomePage = value; } }

    [SwaggerParameter("<i>Contains</i> : title")]
    public override string? Search { get; set; }

    public FaqGroupPageableParam() : base(EFaqGroupSort.UpdatedAt) { }

    protected override Expression<Func<FaqGroup, bool>> ToSearchPredicate(string search)
    {
        return FaqGroup.QTitleContains(search);
    }

    public override Expression<Func<FaqGroup, bool>> ToPredicate()
    {
        Expression<Func<FaqGroup, bool>> result = base.ToPredicate();

        if (_showAtHomePage != null)
        {
            result = result.And(FaqGroup.QShowAtHomePageEquals((bool)_showAtHomePage));
        }

        return result;
    }
}
