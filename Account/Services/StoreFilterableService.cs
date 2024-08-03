using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using KanzApi.Product.Services;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using KanzApi.Vendors.Oto.Services;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Account.Services;

public class StoreFilterableService(IStoreRepository repository,
IMapper mapper, IPrincipalService principalService, ISaleItemFilterableService saleItemFilteredService,
ICodeGenerator codeGenerator, IOtoPickupLocationService otoPickupLocationService)
: FilterableCrudService<Store, int?>(repository), IStoreFilterableService
{

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly ISaleItemFilterableService _saleItemFilteredService = saleItemFilteredService;

    private readonly ICodeGenerator _codeGenerator = codeGenerator;

    private readonly IOtoPickupLocationService _otoPickupLocationService = otoPickupLocationService;

    public StoreResponse Add(StoreRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        if (FindAll(e => e.Name == request.Name, null).Any())
        {
            throw new DuplicateStoreError("Duplicate store name");
        }

        Store entity = _mapper.Map<Store>(request);
        entity.Code = _codeGenerator.Generate("WH", 8);
        entity.Principal = _principalService.GetCurrent();
        entity.Principals.Add(entity.Principal);

        if (entity.Principal.Type != EPrincipalType.Vendor
        && entity.Principal.Type != EPrincipalType.Manufacture)
        {
            throw new PrincipalTypeNotAllowedException((EPrincipalType)entity.Principal.Type!);
        }

        foreach (StorePrincipalRequest employee in request.Employees)
        {
            employee.Type = entity.Principal.Type;
            Principal principal = _principalService.Add(employee);

            entity.Principals.Add(principal);
        }

        OtoPickupLocationResponse otoResponse = _otoPickupLocationService.Create(entity);
        entity.WarehouseId = otoResponse.WarehouseId;

        entity = Add(entity);

        StoreResponse response = _mapper.Map<StoreResponse>(entity);

        scope.Complete();

        return response;
    }

    public StoreResponse Edit(int id, StoreRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Store entity = GetById(id);
        _mapper.Map(request, entity);

        foreach (StorePrincipalRequest employee in request.Employees)
        {
            employee.Type = entity.Principal!.Type;

            Principal? principal = entity.Principals.Find(e => e.Email!.Equals(employee.Email));
            if (principal != null)
            {
                _mapper.Map(employee, principal);
                _principalService.Edit(principal);
            }
            else
            {
                principal = _principalService.Add(employee);
                entity.Principals.Add(principal);
            }
        }

        try
        {
            _otoPickupLocationService.Update(entity);
        }
        catch (OtoErrorException)
        {
            OtoPickupLocationResponse otoResponse = _otoPickupLocationService.Create(entity);
            entity.WarehouseId = otoResponse.WarehouseId;
        }

        entity = Edit(entity);

        StoreResponse response = _mapper.Map<StoreResponse>(entity);

        scope.Complete();

        return response;
    }

    public override Store Remove(Store entity)
    {
        entity.Status = EActivableStatus.Deleted;
        return Edit(entity);
    }

    public StoreResponse RemoveModelById(int id)
    {
        Store entity = GetById(id);

        IEnumerable<SaleItem> saleItems = _saleItemFilteredService.FindAllByStoreId(id);
        if (!saleItems.Any())
        {
            Remove(entity);
        }
        else
        {
            throw new DuplicateStoreError("this store currently has active product");
        }

        return _mapper.Map<StoreResponse>(entity);
    }

    public Store Activate(Store entity)
    {
        entity.Status = EActivableStatus.Active;
        return Edit(entity);
    }

    public Store Inactivate(Store entity)
    {
        entity.Status = EActivableStatus.Inactive;
        return Edit(entity);
    }

    public StoreResponse GetModelById(int id)
    {
        Store entity = GetById(id);
        return _mapper.Map<StoreResponse>(entity);
    }

    protected override Expression<Func<Store, bool>> Filter(Expression<Func<Store, bool>>? predicate)
    {
        Principal principal = _principalService.GetCurrent();

        if (principal.Type == EPrincipalType.Vendor || principal.Type == EPrincipalType.Manufacture)
        {
            Expression<Func<Store, bool>> filterPredicate = Store.QPrincipalIdEquals((int)principal.Id!)
            .Or(Store.QPrincipalIdsContains((int)principal.Id!)).And(Store.QStatusNotEquals(EActivableStatus.Deleted));
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else
        {
            return predicate ?? Expressions.True<Store>();
        }
    }

    public PaginatedResponse<StoreResponse> FindAllModels(StorePageableParam param)
    {
        PaginatedEntity<Store> pEntity = FindAll(param);
        IEnumerable<StoreResponse> models = pEntity.Content.Select(_mapper.Map<StoreResponse>);

        return PaginatedResponse<StoreResponse>.From(pEntity, models);
    }

    public IEnumerable<NameableResponse> FindAllModels(StoreSearchableParam param)
    {
        IEnumerable<Store> entities = FindAll(param);
        return entities.Select(_mapper.Map<NameableResponse>).ToList();
    }
}
