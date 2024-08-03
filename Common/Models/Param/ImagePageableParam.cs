using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class ImagePageableParam : PageableParam<EImageSort, Image>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public ImagePageableParam() : base(EImageSort.UpdatedAt) { }

    protected override Expression<Func<Image, bool>> ToSearchPredicate(string search)
    {
        return Image.QNameContains(search);
    }
}
