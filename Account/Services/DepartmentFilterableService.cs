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

public class DepartmentFilterableService(IDepartmentRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalDetailFilterableService principalDetailService, IPrincipalService principalService)
: FilterableCrudService<Department, int?>(repository), IDepartmentFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    protected readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    public DepartmentResponse Add(DepartmentRequest request)
    {
        Department entity = _mapper.Map<Department>(request);

        PrincipalDetail principalDetail = _principalDetailService.GetByCurrentPrincipal();
        entity.PrincipalDetail = principalDetail;

        entity = Add(entity);
        return _mapper.Map<DepartmentResponse>(entity);
    }

    public DepartmentResponse Edit(int id, DepartmentRequest request)
    {
        Department entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<DepartmentResponse>(entity);
    }

    public override Department Remove(Department entity)
    {
        entity.Principals.Clear();

        return base.Remove(entity);
    }

    public DepartmentResponse RemoveModelById(int id)
    {
        Department entity = RemoveById(id);
        return _mapper.Map<DepartmentResponse>(entity);
    }

    public DepartmentResponse GetModelById(int id)
    {
        Department entity = GetById(id);
        return _mapper.Map<DepartmentResponse>(entity);
    }

    protected override Expression<Func<Department, bool>> Filter(Expression<Func<Department, bool>>? predicate)
    {
        Expression<Func<Department, bool>>? filterPredicate = Department.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<DepartmentResponse> FindAllModels(DepartmentPageableParam param)
    {
        PaginatedEntity<Department> pEntity = FindAll(param);
        IEnumerable<DepartmentResponse> models = pEntity.Content.Select(_mapper.Map<DepartmentResponse>);

        return PaginatedResponse<DepartmentResponse>.From(pEntity, models);
    }
}
