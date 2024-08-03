using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class BannerPageableParam : PageableParam<EBannerSort, Banner>
{

    [SwaggerParameter("<i>Contains</i> : title")]
    public override string? Search { get; set; }

    public BannerPageableParam() : base(EBannerSort.UpdatedAt) { }

    protected override Expression<Func<Banner, bool>> ToSearchPredicate(string search)
    {
        return Banner.QTitleContains(search);
    }
}
