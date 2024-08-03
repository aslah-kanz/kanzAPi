using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Account.Services;

public class PrincipalBankFilterableService(IPrincipalBankRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService)
: FilterableCrudService<PrincipalBank, int?>(repository), IPrincipalBankFilterableService
{

    protected readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    public PrincipalBankResponse Add(PrincipalBankRequest request)
    {
        PrincipalBank entity = _mapper.Map<PrincipalBank>(request);

        entity.Principal = _principalService.GetCurrent();

        entity = Add(entity);
        return _mapper.Map<PrincipalBankResponse>(entity);
    }

    public PrincipalBankResponse Edit(int id, PrincipalBankRequest request)
    {
        PrincipalBank entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<PrincipalBankResponse>(entity);
    }

    public PrincipalBankResponse RemoveModelById(int id)
    {
        PrincipalBank entity = RemoveById(id);
        return _mapper.Map<PrincipalBankResponse>(entity);
    }

    public PrincipalBankResponse GetModelById(int id)
    {
        PrincipalBank entity = GetById(id);
        return _mapper.Map<PrincipalBankResponse>(entity);
    }

    protected override Expression<Func<PrincipalBank, bool>> Filter(Expression<Func<PrincipalBank, bool>>? predicate)
    {
        Expression<Func<PrincipalBank, bool>> filterPredicate = PrincipalBank.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<PrincipalBankResponse> FindAllModels(PrincipalBankPageableParam param)
    {
        PaginatedEntity<PrincipalBank> pEntity = FindAll(param);
        IEnumerable<PrincipalBankResponse> models = pEntity.Content.Select(_mapper.Map<PrincipalBankResponse>);

        return PaginatedResponse<PrincipalBankResponse>.From(pEntity, models);
    }
}
