using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class BlogPageableParam : PageableParam<EBlogSort, Blog>
{

    [SwaggerParameter("<i>Contains</i> : title")]
    public override string? Search { get; set; }

    public BlogPageableParam() : base(EBlogSort.UpdatedAt) { }

    protected override Expression<Func<Blog, bool>> ToSearchPredicate(string search)
    {
        return Blog.QTitleContains(search);
    }
}
