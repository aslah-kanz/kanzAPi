using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Account.Services;

public class PrincipalAddressFilterableService(IPrincipalAddressRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService)
: FilterableCrudService<PrincipalAddress, int?>(repository), IPrincipalAddressFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    public PrincipalAddressResponse Add(PrincipalAddressRequest request)
    {
        PrincipalAddress entity = _mapper.Map<PrincipalAddress>(request);

        entity.Principal = _principalService.GetCurrent();

        using TransactionScope scope = Transactions.Create();

        IEnumerable<PrincipalAddress> defaultEntities = FindAll(PrincipalAddress.QDefaultAddressEquals(true), null);
        entity.DefaultAddress = !defaultEntities.Any();

        entity = Add(entity);
        PrincipalAddressResponse response = _mapper.Map<PrincipalAddressResponse>(entity);

        scope.Complete();

        return response;
    }

    public PrincipalAddressResponse Edit(int id, PrincipalAddressRequest request)
    {
        PrincipalAddress entity = GetById(id);
        _mapper.Map(request, entity);

        using TransactionScope scope = Transactions.Create();

        entity = Edit(entity);
        PrincipalAddressResponse response = _mapper.Map<PrincipalAddressResponse>(entity);

        scope.Complete();

        return response;
    }

    public PrincipalAddressResponse ChangeDefault(int id)
    {
        PrincipalAddress entity = GetById(id);

        using TransactionScope scope = Transactions.Create();

        Expression<Func<PrincipalAddress, bool>> predicate = PrincipalAddress.QDefaultAddressEquals(true)
        .And(PrincipalAddress.QIdNotEquals((int)entity.Id!));

        IEnumerable<PrincipalAddress> defaultEntities = FindAll(predicate, null);
        foreach (PrincipalAddress defaultEntity in defaultEntities)
        {
            defaultEntity.DefaultAddress = false;
            Edit(defaultEntity);
        }

        entity.DefaultAddress = true;
        entity = Edit(entity);

        PrincipalAddressResponse response = _mapper.Map<PrincipalAddressResponse>(entity);

        scope.Complete();

        return response;
    }

    public PrincipalAddressResponse RemoveModelById(int id)
    {
        PrincipalAddress entity = RemoveById(id);
        return _mapper.Map<PrincipalAddressResponse>(entity);
    }

    public PrincipalAddressResponse GetModelById(int id)
    {
        PrincipalAddress entity = GetById(id);
        return _mapper.Map<PrincipalAddressResponse>(entity);
    }

    public PrincipalAddress GetDefault()
    {
        return FindByPredicate(PrincipalAddress.QDefaultAddressEquals(true))
        ?? throw EntityNotFoundException.From(typeof(PrincipalAddress), "Default Address", true);
    }

    protected override Expression<Func<PrincipalAddress, bool>> Filter(Expression<Func<PrincipalAddress, bool>>? predicate)
    {
        Expression<Func<PrincipalAddress, bool>>? filterPredicate = PrincipalAddress.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<PrincipalAddressItem> FindAllModels(PrincipalAddressPageableParam param)
    {
        PaginatedEntity<PrincipalAddress> pEntity = FindAll(param);
        IEnumerable<PrincipalAddressItem> models = pEntity.Content.Select(_mapper.Map<PrincipalAddressItem>);

        return PaginatedResponse<PrincipalAddressItem>.From(pEntity, models);
    }
}
