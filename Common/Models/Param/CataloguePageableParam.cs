using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class CataloguePageableParam : PageableParam<ECatalogueSort, Catalogue>
{

    [SwaggerParameter("<i>Contains</i> : title")]
    public override string? Search { get; set; }

    public CataloguePageableParam() : base(ECatalogueSort.UpdatedAt) { }

    protected override Expression<Func<Catalogue, bool>> ToSearchPredicate(string search)
    {
        return Catalogue.QTitleContains(search);
    }
}
