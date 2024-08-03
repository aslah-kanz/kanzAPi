using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Services;
using MapsterMapper;

namespace KanzApi.Account.Services;

public class RoleService(IRoleRepository repository, IMapper mapper)
: CrudService<Role, int?>(repository), IRoleService
{

    private readonly IMapper _mapper = mapper;

    public RoleResponse Add(RoleRequest request)
    {
        Role entity = _mapper.Map<Role>(request);
        entity = Add(entity);

        return _mapper.Map<RoleResponse>(entity);
    }

    public RoleResponse Edit(int id, RoleRequest request)
    {
        Role entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<RoleResponse>(entity);
    }

    public RoleResponse RemoveModelById(int id)
    {
        Role entity = RemoveById(id);
        return _mapper.Map<RoleResponse>(entity);
    }

    public RoleResponse GetModelById(int id)
    {
        Role entity = GetById(id);
        return _mapper.Map<RoleResponse>(entity);
    }

    public IEnumerable<RoleItem> FindAllModels(RoleSearchableParam param)
    {
        IEnumerable<Role> entities = FindAll(param);
        return entities.Select(_mapper.Map<RoleItem>);
    }
}
