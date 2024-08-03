using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Repositories;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Product.Services;

public class SaleItemFilterableService(ISaleItemRepository repository,
IMapper mapper, ISaleItemSyncableService service, IPrincipalService principalService,
IStoreService storeService, ISessionService sessionService, IConfiguration configuration)
: FilterableCrudService<SaleItem, long?>(repository), ISaleItemFilterableService
{
    private readonly IMapper _mapper = mapper;

    private readonly ISaleItemSyncableService _service = service;

    private readonly ISessionService _sessionService = sessionService;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IConfiguration _configuration = configuration;

    private readonly IStoreService _storeService = storeService;

    private void CalculatePrice(SaleItem entity)
    {
        decimal fee = 1 + _configuration.GetValue<decimal>("KanzApi:CommissionRate");
        decimal tax = 1 + _configuration.GetValue<decimal>("KanzApi:InclusiveTax");

        if (entity.DiscountPrice > 0)
        {
            entity.Price = Math.Round((decimal)entity.DiscountPrice * fee * tax, 2);
            entity.OriginalPrice = Math.Round((decimal)entity.MinPrice! * fee * tax, 2);
        }
        else
        {
            entity.Price = Math.Round((decimal)entity.MinPrice! * fee * tax, 2);
            entity.OriginalPrice = null;
        }
    }

    public override SaleItem Add(SaleItem entity)
    {
        return _service.Add(entity);
    }

    public override SaleItem Edit(SaleItem entity)
    {
        return _service.Edit(entity);
    }

    public override SaleItem Remove(SaleItem entity)
    {
        return _service.Remove(entity);
    }

    public SaleItemResponse Add(SaleItemRequest request)
    {
        SaleItem entity = _mapper.Map<SaleItem>(request);
        CalculatePrice(entity);

        using TransactionScope scope = Transactions.Create();

        _storeService.IncreaseSaleItemCount(entity.Store!);

        entity = Add(entity);

        SaleItemResponse response = _mapper.Map<SaleItemResponse>(entity);

        scope.Complete();

        return response;
    }

    public SaleItemResponse ChangeEnabled(long id, bool enabled)
    {
        SaleItem entity = GetById(id);
        entity.Enabled = enabled;

        entity = Edit(entity);

        return _mapper.Map<SaleItemResponse>(entity);
    }

    public SaleItemResponse Edit(long id, SaleItemRequest request)
    {
        SaleItem entity = GetById(id);
        _mapper.Map(request, entity);

        CalculatePrice(entity);

        entity = Edit(entity);

        return _mapper.Map<SaleItemResponse>(entity);
    }

    public SaleItemResponse RemoveModelById(long id)
    {
        SaleItem entity = RemoveById(id);

        return _mapper.Map<SaleItemResponse>(entity);
    }

    public SaleItemResponse Activate(long id)
    {
        SaleItem entity = GetById(id);
        entity.Status = EActivableStatus.Active;
        entity = Edit(entity);

        return _mapper.Map<SaleItemResponse>(entity);
    }

    public SaleItemResponse Inactivate(long id)
    {
        SaleItem entity = GetById(id);
        entity.Status = EActivableStatus.Inactive;
        entity = Edit(entity);

        return _mapper.Map<SaleItemResponse>(entity);
    }

    public SaleItemResponse GetModelById(long id)
    {
        SaleItem entity = GetById(id);
        return _mapper.Map<SaleItemResponse>(entity);
    }

    public IEnumerable<SaleItem> FindAllByProductId(int id)
    {
        return FindAll(SaleItem.QProductIdEquals(id), null);
    }

    public IEnumerable<SaleItem> FindAllByStoreId(int id)
    {
        return FindAll(SaleItem.QStoreIdEquals(id), null);
    }

    protected override Expression<Func<SaleItem, bool>> Filter(Expression<Func<SaleItem, bool>>? predicate)
    {
        int? principalId = _sessionService.CurrentPrincipalId();
        if (principalId != null)
        {
            Principal principal = _principalService.GetById(principalId);
            if (principal.Type == EPrincipalType.Manufacture || principal.Type == EPrincipalType.Vendor)
            {
                Expression<Func<SaleItem, bool>> filterPredicate = SaleItem.QPrincipalIdEquals((int)principalId)
                .Or(SaleItem.QPrincipalIdsContains((int)principalId));
                return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
            }
        }

        return predicate ?? Expressions.True<SaleItem>();
    }

    public PaginatedResponse<SaleItemResponse> FindAllModels(SaleItemPageableParam param)
    {
        PaginatedEntity<SaleItem> pEntity = FindAll(param);
        IEnumerable<SaleItemResponse> models = pEntity.Content.Select(_mapper.Map<SaleItemResponse>);

        return PaginatedResponse<SaleItemResponse>.From(pEntity, models);
    }

    private PaginatedResponse<SaleItemStoreResponse> FindAllModels(SaleItemPageableParam param, Expression<Func<SaleItem, bool>> predicate)
    {
        Page page = new(param.Page, param.Size);
        predicate = param.ToPredicate().And(predicate);

        PaginatedEntity<SaleItem> pEntity = FindAll(page, predicate, Sort.From(param));
        IEnumerable<SaleItemStoreResponse> models = pEntity.Content.Select(_mapper.Map<SaleItemStoreResponse>);

        return PaginatedResponse<SaleItemStoreResponse>.From(pEntity, models);
    }

    public PaginatedResponse<SaleItemStoreResponse> FindAllModels(int productId, SaleItemPageableParam param)
    {
        return FindAllModels(param, SaleItem.QProductIdEquals(productId));
    }
}
