using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Account.Services;

public class PrincipalDetailFilterableService(IPrincipalDetailRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
IPrincipalDetailItemService principalDetailItemService)
: FilterableCrudService<PrincipalDetail, int?>(repository), IPrincipalDetailFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailItemService _principalDetailItemService = principalDetailItemService;

    public PrincipalDetailResponse Add(Principal principal, PrincipalDetailRequest request)
    {
        PrincipalDetail entity = _mapper.Map<PrincipalDetail>(request);

        using TransactionScope scope = Transactions.Create();

        entity.PrincipalId = principal.Id;
        entity.Principals.Add(principal);
        entity = Add(entity);

        foreach (PrincipalDetailItemRequest item in request.Items)
        {
            _principalDetailItemService.Add(entity, item);
        }

        PrincipalDetailResponse response = _mapper.Map<PrincipalDetailResponse>(entity);

        scope.Complete();

        return response;
    }

    public PrincipalDetailResponse Add(PrincipalDetailRequest request)
    {
        Principal principal = _principalService.GetCurrent();
        return Add(principal, request);
    }

    public CompanyMemberResponse AddMember(CompanyMemberRequest request)
    {
        PrincipalDetail entity = GetByCurrentPrincipal();

        using TransactionScope scope = Transactions.Create();

        Principal principal = _principalService.Add(request);
        entity.Principals.Add(principal);
        Edit(entity);

        CompanyMemberResponse response = _mapper.Map<CompanyMemberResponse>(principal);

        scope.Complete();

        return response;
    }

    public PrincipalDetailResponse Edit(int id, PrincipalDetailRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        PrincipalDetail entity = GetById(id);
        _mapper.Map(request, entity);
        entity = Edit(entity);

        foreach (PrincipalDetailItemRequest item in request.Items)
        {
            PrincipalDetailItem? property = entity.Properties.Find(i => i.Description!.Equals(item.Description));
            if (property != null)
            {
                _mapper.Map(item, property);
                _principalDetailItemService.Edit(property!);
            }
            else
            {
                property = _principalDetailItemService.Add(entity, item);
            }
        }

        PrincipalDetailResponse response = _mapper.Map<PrincipalDetailResponse>(entity);

        scope.Complete();

        return response;
    }

    public PrincipalDetailResponse GetModelById(int id)
    {
        PrincipalDetail entity = GetById(id);
        return _mapper.Map<PrincipalDetailResponse>(entity);
    }

    public PrincipalDetail? FindByCurrentPrincipal()
    {
        return FindByPredicate(Expressions.True<PrincipalDetail>());
    }

    public PrincipalDetail GetByCurrentPrincipal()
    {
        return FindByCurrentPrincipal()
        ?? throw EntityNotFoundException.From(typeof(PrincipalDetail), "Principal ID", (int)_sessionService.CurrentPrincipalId()!);
    }

    protected override Expression<Func<PrincipalDetail, bool>> Filter(Expression<Func<PrincipalDetail, bool>>? predicate)
    {
        Expression<Func<PrincipalDetail, bool>> filterPredicate = PrincipalDetail.QPrincipalIdsContains((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<PrincipalDetailResponse> FindAllModels(PrincipalDetailPageableParam param)
    {
        PaginatedEntity<PrincipalDetail> pEntity = FindAll(param);
        IEnumerable<PrincipalDetailResponse> models = pEntity.Content.Select(_mapper.Map<PrincipalDetailResponse>);

        return PaginatedResponse<PrincipalDetailResponse>.From(pEntity, models);
    }

    public PaginatedResponse<CustomerItem> FindAllCompanyMembers(CompanyMemberPageableParam param)
    {
        PrincipalDetail entity = GetByCurrentPrincipal();

        Expression<Func<Principal, bool>>? predicate = param.ToPredicate()
        .And(Principal.QStatusNotEquals(EPrincipalStatus.Disabled)
        .And(Principal.QPrincipalDetailIdsContains((int)entity.Id!)));

        Page page = new(param.Page, param.Size);

        PaginatedEntity<Principal> pEntity = _principalService.FindAll(page, predicate, Sort.From(param));
        IEnumerable<CustomerItem> models = pEntity.Content.Select(_mapper.Map<CustomerItem>);

        return PaginatedResponse<CustomerItem>.From(pEntity, models);
    }
}
