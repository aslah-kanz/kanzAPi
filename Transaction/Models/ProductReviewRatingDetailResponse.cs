using KanzApi.Common.Models;

namespace KanzApi.Transaction.Models;

public class ProductReviewRatingDetailResponse : PaginatedResponse<ProductReviewItem>
{
	public ProductReviewProductResponse? Product { get; set; }

	public int? TotalRating { get; set; }

    public int? ReviewerCount { get; set; }

    public double? RatingAverage { get; set; }

    public int? Rating1 { get; set; }

    public int? Rating2 { get; set; }

    public int? Rating3 { get; set; }

    public int? Rating4 { get; set; }

    public int? Rating5 { get; set; }
}
