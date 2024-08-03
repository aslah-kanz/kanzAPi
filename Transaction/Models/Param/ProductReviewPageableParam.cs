using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Models.Param;

public class ProductReviewPageableParam : PageableParam<EProductReviewSort, ProductReview>
{

    private int? _rating;

    public int? Rating { get { return _rating; } set { _rating = value; } }

    [SwaggerParameter("<i>Contains</i> : comment")]
    public override string? Search { get; set; }

    public ProductReviewPageableParam() : base(EProductReviewSort.UpdatedAt) { }

    protected override Expression<Func<ProductReview, bool>> ToSearchPredicate(string search)
    {
        return ProductReview.QCommentContains(search);
    }

    public override Expression<Func<ProductReview, bool>> ToPredicate()
    {
        Expression<Func<ProductReview, bool>> result = base.ToPredicate();

        if (_rating != null)
        {
            result = result.And(ProductReview.QRatingEquals((int)_rating));
        }
        return result;
    }
}
