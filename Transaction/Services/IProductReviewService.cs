using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IProductReviewService : IFilterableCrudService<ProductReview, Guid?>
{

    ProductReviewPaginatedResponse FindAllModels(Guid purchaseQuoteId, ProductReviewPageableParam param);

    ProductReviewPaginatedResponse FindAllModels(string productSlug, ProductReviewPageableParam param);

    PaginatedResponse<ProductReviewItem> FindAllModels(int storeId, int productId, ProductReviewPageableParam param);

}
