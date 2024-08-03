using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IProductReviewFilterableService : IFilterableCrudService<ProductReview, Guid?>
{

    ProductReviewResponse Add(Guid purchaseQuoteId, ProductReviewRequest request);

    ProductReviewResponse Edit(Guid purchaseQuoteId, Guid id, ProductReviewRequest request);

    ProductReviewResponse RemoveModelById(Guid purchaseQuoteId, Guid id);

    ProductReviewResponse GetModelById(Guid purchaseQuoteId, Guid id);

    ProductReviewRatingsSummary? FindSummary(int storeId, int productId);

	ProductReviewRatingDetailResponse? GetDetail(int storeId, int productId, ProductReviewDetailPageableParam param);

	PaginatedResponse<ProductReviewSummary> FindAllSummaries(ProductReviewSummaryPageableParam param);

}
