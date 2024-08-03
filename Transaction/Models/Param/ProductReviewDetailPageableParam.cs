using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class ProductReviewDetailPageableParam : PageableParam<EProductReviewSort, ProductReview>
{

    private List<int>? _ratings;

    public List<int>? Ratings { get { return _ratings; } set { _ratings = value; } }

    [SwaggerParameter("<i>Contains</i> : comment")]
    public override string? Search { get; set; }

    public ProductReviewDetailPageableParam() : base(EProductReviewSort.UpdatedAt) { }

    protected override Expression<Func<ProductReview, bool>> ToSearchPredicate(string search)
    {
        return ProductReview.QCommentContains(search);
    }

    public override Expression<Func<ProductReview, bool>> ToPredicate()
    {
        Expression<Func<ProductReview, bool>> result = base.ToPredicate();

        if (_ratings.Count != 0)
        {
            result = result.And(ProductReview.QRatingsEquals(_ratings));
        }
        return result;
    }
}
