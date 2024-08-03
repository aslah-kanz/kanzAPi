using KanzApi.Common.Models.Param;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models.Param;

public class ProductReviewSummaryPageableParam : PageableParam<EVendorProductReviewSort, ProductReview>
{

    public ProductReviewSummaryPageableParam() : base(EVendorProductReviewSort.RatingAverage) { }
}
