using KanzApi.Common.Entities;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Common.Models.Param;

public class WebPageSearchableParam : SearchableParam<EWebPageSort, WebPage>
{

    [SwaggerParameter("<i>Contains</i> : title, slug")]
    public override string? Search { get; set; }

    public WebPageSearchableParam() : base(EWebPageSort.UpdatedAt, EOrder.Asc) { }

    protected override Expression<Func<WebPage, bool>> ToSearchPredicate(string search)
    {
        return WebPage.QTitleContains(search).Or(
            WebPage.QSlugContains(search));
    }
}
