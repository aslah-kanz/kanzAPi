using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Services;
using KanzApi.Common.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class ProductReviewFilterableService(IProductReviewRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
IImageService imageService, IProductSyncableService productService,
IPurchaseQuoteFilterableService purchaseQuoteService, IStoreFilterableService storeService)
: FilterableCrudService<ProductReview, Guid?>(repository), IProductReviewFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IImageService _imageService = imageService;

    private readonly IProductSyncableService _productService = productService;

    private readonly IPurchaseQuoteFilterableService _purchaseQuoteService = purchaseQuoteService;

    private readonly IStoreFilterableService _storeService = storeService;

    public ProductReviewResponse Add(Guid purchaseQuoteId, ProductReviewRequest request)
    {
        ProductReview entity = _mapper.Map<ProductReview>(request);
        entity.Principal = _principalService.GetCurrent();
        var result = FindAll().Any(e => e.PurchaseQuoteId == purchaseQuoteId);
        if (result)
        {
            throw new ReviewSubmittedException();
        }

        using TransactionScope scope = Transactions.Create();

        foreach (IFormFile file in request.Files!)
        {
            Image image = _imageService.AddWithRandomName(file, EImageGroup.ProductReview);
            entity.Images.Add(image);
        }

        PurchaseQuote purchaseQuote = _purchaseQuoteService.GetById(purchaseQuoteId);
        entity.PurchaseQuote = purchaseQuote;


        CustomerOrder order = purchaseQuote.CustomerOrder!;
        if (order.PrincipalId != entity.PrincipalId)
        {
            throw new PrincipalNotAllowedException();
        }

        entity = Add(entity);
        ProductReviewResponse response = _mapper.Map<ProductReviewResponse>(entity);

        scope.Complete();

        return response;
    }

    public ProductReviewResponse Edit(Guid purchaseQuoteId, Guid id, ProductReviewRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        ProductReview entity = GetById(purchaseQuoteId, id);
        _mapper.Map(request, entity);

        foreach (IFormFile file in request.Files!)
        {
            Image image = _imageService.AddWithRandomName(file, EImageGroup.ProductReview);
            entity.Images.Add(image);
        }

        entity = Edit(entity);
        ProductReviewResponse response = _mapper.Map<ProductReviewResponse>(entity);

        scope.Complete();

        return response;
    }

    public ProductReviewResponse RemoveModelById(Guid purchaseQuoteId, Guid id)
    {
        ProductReview entity = GetById(purchaseQuoteId, id);
        entity = Remove(entity);

        return _mapper.Map<ProductReviewResponse>(entity);
    }

    public ProductReview GetById(Guid purchaseQuoteId, Guid id)
    {
        return FindById(id, ProductReview.QPurchaseQuoteIdEquals(purchaseQuoteId))
        ?? throw EntityNotFoundException.From(typeof(ProductReview),
        new Dictionary<string, object?> { { "Purchase Quote ID", purchaseQuoteId }, { "ID", id } });
    }

    public ProductReviewResponse GetModelById(Guid purchaseQuoteId, Guid id)
    {
        ProductReview entity = GetById(purchaseQuoteId, id);
        return _mapper.Map<ProductReviewResponse>(entity);
    }

    protected override Expression<Func<ProductReview, bool>> Filter(Expression<Func<ProductReview, bool>>? predicate)
    {
        Expression<Func<ProductReview, bool>> filterPredicate = ProductReview.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public ProductReviewRatingsSummary? FindSummary(int storeId, int productId)
    {
        HashSet<int> storeIds = _principalService.GetCurrent().Stores.Select(e => (int)e.Id!).ToHashSet();
        if (!storeIds.Contains(storeId))
        {
            throw EntityNotFoundException.From(typeof(Store), "ID", storeId);
        }

        Expression<Func<ProductReview, bool>> predicate = ProductReview.QProductIdEquals(productId)
        .And(ProductReview.QStoreIdEquals(storeId));

        ProductReviewRatingsSummary entity = repository.FindSummary(predicate) ?? new();

        entity.Store = _mapper.Map<StoreResponse>(_storeService.GetById(storeId));
        entity.Product = _mapper.Map<ProductReviewProductResponse>(_productService.GetById(productId));

        return entity;
    }

    public ProductReviewRatingDetailResponse? GetDetail(int storeId, int productId, ProductReviewDetailPageableParam param)
    {
        HashSet<int> storeIds = _principalService.GetCurrent().Stores.Select(e => (int)e.Id!).ToHashSet();
        if (!storeIds.Contains(storeId))
        {
            throw EntityNotFoundException.From(typeof(Store), "ID", storeId);
        }

        Page page = new(param.Page, param.Size);
        Expression<Func<ProductReview, bool>> predicate = ProductReview.QProductIdEquals(productId)
        .And(ProductReview.QStoreIdEquals(storeId));

        ProductReviewRatingsSummary summary = repository.FindSummary(predicate) ?? new();
        PaginatedEntity<ProductReviewItem> entity = repository.GetDetail(page, predicate, Sort.From(param));
        ProductReviewRatingDetailResponse result = new()
        {
            Product = _mapper.Map<ProductReviewProductResponse>(_productService.GetById(productId)),
            Content = entity.Content,
            TotalRating = summary.TotalRating,
            ReviewerCount = summary.ReviewerCount,
            RatingAverage = summary.RatingAverage.HasValue ? Math.Round(summary.RatingAverage.Value, 1, MidpointRounding.ToZero) : 0.0d,
            Rating1 = summary.Rating1,
            Rating2 = summary.Rating2,
            Rating3 = summary.Rating3,
            Rating4 = summary.Rating4,
            Rating5 = summary.Rating5,
            Page = entity.Page,
            Size = entity.Size,
            TotalCount = entity.Count
        };

        return result;
    }

    public PaginatedResponse<ProductReviewSummary> FindAllSummaries(ProductReviewSummaryPageableParam param)
    {
        ISet<int> storeIds = _principalService.GetCurrent().Stores.Select(e => (int)e.Id!).ToHashSet();

        // prevent if page less than 1 is inputted, keep set the page to 1 if that happen
        var activePage = (param.Page < 1) ? 1 : param.Page;

        Page page = new(activePage, param.Size);
        Expression<Func<ProductReview, bool>> predicate = param.ToPredicate().And(ProductReview.QStoreIdEquals(storeIds));

        PaginatedEntity<ProductReviewSummary> pEntity = repository.FindAllSummaries(page, predicate, Sort.From(param));
        foreach (ProductReviewSummary entity in pEntity.Content)
        {
            entity.Store = _mapper.Map<StoreResponse>(_storeService.GetById(entity.StoreId));
            entity.Product = _mapper.Map<ProductReviewProductResponse>(_productService.GetById(entity.ProductId));
        }

        return PaginatedResponse<ProductReviewSummary>.From(pEntity);
    }
}
