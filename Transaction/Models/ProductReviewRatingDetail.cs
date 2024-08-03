namespace KanzApi.Transaction.Models;

public class ProductReviewRatingDetail
{
	public ProductReviewProductResponse? Product { get; set; }

    public IEnumerable<ProductReviewItem>? Reviews { get; set; }

}
