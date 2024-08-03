using KanzApi.Common.Models;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models;

public class ProductReviewPaginatedResponse : PaginatedResponse<ProductReviewItem>
{

    public double RatingAverage { get; set; }

    public static ProductReviewPaginatedResponse From(
        PaginatedEntity<ProductReview> pEntity, IEnumerable<ProductReviewItem> content, double ratingAverage)
    {
        return new()
        {
            Content = content,
            Page = pEntity.Page,
            Size = pEntity.Size,
            TotalCount = pEntity.Count,
            RatingAverage = ratingAverage
        };
    }
}
