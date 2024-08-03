using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class WishListFilterableService(IWishListRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService)
: FilterableCrudService<WishList, int?>(repository), IWishListFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    public WishListResponse Add(WishListRequest request)
    {
        WishList entity = _mapper.Map<WishList>(request);
        entity.Principal = _principalService.GetById(_sessionService.CurrentPrincipalId() ?? 0);

        entity = Add(entity);

        return _mapper.Map<WishListResponse>(entity);
    }

    public WishListResponse Edit(int id, WishListRequest request)
    {
        WishList entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<WishListResponse>(entity);
    }

    public WishListResponse RemoveModelById(int id)
    {
        WishList entity = RemoveById(id);
        return _mapper.Map<WishListResponse>(entity);
    }

    public WishListResponse RemoveModelByProductId(int productId)
    {
        WishList entity = GetByProductId(productId);

        entity = Remove(entity);

        return _mapper.Map<WishListResponse>(entity);
    }

    public WishList GetByProductId(int productId)
    {
        return FindByPredicate(WishList.QProductIdEquals(productId))
        ?? throw EntityNotFoundException.From(typeof(ProductReview), "Product ID", productId);
    }

    public WishListResponse GetModelById(int id)
    {
        WishList entity = GetById(id);
        return _mapper.Map<WishListResponse>(entity);
    }

    protected override Expression<Func<WishList, bool>> Filter(Expression<Func<WishList, bool>>? predicate)
    {
        Expression<Func<WishList, bool>> filterPredicate = WishList.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<WishListResponse> FindAllModels(WishListPageableParam param)
    {
        PaginatedEntity<WishList> pEntity = FindAll(param);
        IEnumerable<WishListResponse> models = pEntity.Content.Select(_mapper.Map<WishListResponse>);

        return PaginatedResponse<WishListResponse>.From(pEntity, models);
    }

    public IEnumerable<int> FindAllProductIds()
    {
        IEnumerable<WishList> pEntity = FindAll();

        return pEntity.Select(e => (int)e.ProductId!);
    }
}
