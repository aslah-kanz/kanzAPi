using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class WishListPageableParam : PageableParam<EWishListSort, WishList>
{

    [SwaggerParameter("<i>Contains</i> : productName, slug, mpn")]
    public override string? Search { get; set; }

    public WishListPageableParam() : base(EWishListSort.UpdatedAt) { }

    protected override Expression<Func<WishList, bool>> ToSearchPredicate(string search)
    {
        return WishList.QProductNameContains(search).Or(
                WishList.QProductSlugContains(search),
                WishList.QMpnContains(search));
    }
}
