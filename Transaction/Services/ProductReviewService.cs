using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class ProductReviewService(IProductReviewRepository repository, IMapper mapper,
IPrincipalService principalService) : CrudService<ProductReview, Guid?>(repository), IProductReviewService
{

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private ProductReviewPaginatedResponse FindAllModels(ProductReviewPageableParam param, Expression<Func<ProductReview, bool>> predicate)
    {
        Page page = new(param.Page, param.Size);
        predicate = param.ToPredicate().And(predicate);

        PaginatedEntity<ProductReview> pEntity = FindAll(page, predicate, Sort.From(param));
        IEnumerable<ProductReviewItem> models = pEntity.Content.Select(_mapper.Map<ProductReviewItem>);

        double ratingAverage = models.Any() ? repository.RatingAverage(predicate) : 0;

        return ProductReviewPaginatedResponse.From(pEntity, models, ratingAverage);
    }

    public ProductReviewPaginatedResponse FindAllModels(Guid purchaseQuoteId, ProductReviewPageableParam param)
    {
        return FindAllModels(param, ProductReview.QPurchaseQuoteIdEquals(purchaseQuoteId));
    }

    public ProductReviewPaginatedResponse FindAllModels(string productSlug, ProductReviewPageableParam param)
    {
        return FindAllModels(param, ProductReview.QProductSlugEquals(productSlug));
    }

    public PaginatedResponse<ProductReviewItem> FindAllModels(int storeId, int productId, ProductReviewPageableParam param)
    {
        HashSet<int> storeIds = _principalService.GetCurrent().Stores.Select(e => (int)e.Id!).ToHashSet();
        if (!storeIds.Contains(storeId))
        {
            throw EntityNotFoundException.From(typeof(Store), "ID", storeId);
        }

        Page page = new(param.Page, param.Size);
        Expression<Func<ProductReview, bool>> predicate = param.ToPredicate()
        .And(ProductReview.QProductIdEquals(productId), ProductReview.QStoreIdEquals(storeId));

        PaginatedEntity<ProductReview> pEntity = FindAll(page, predicate, Sort.From(param));
        IEnumerable<ProductReviewItem> models = pEntity.Content.Select(_mapper.Map<ProductReviewItem>);

        return PaginatedResponse<ProductReviewItem>.From(pEntity, models);
    }
}
