using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Repositories;
using KanzApi.Common.Services;
using MapsterMapper;

namespace KanzApi.Account.Services;

public class PrivilegeService(IPrivilegeRepository repository, IMapper mapper)
: CrudService<Privilege, int?>(repository), IPrivilegeService
{

    private readonly IMapper _mapper = mapper;

    public PrivilegeResponse Add(PrivilegeRequest request)
    {
        Privilege entity = _mapper.Map<Privilege>(request);
        entity = Add(entity);

        return _mapper.Map<PrivilegeResponse>(entity);
    }

    public PrivilegeResponse Edit(int id, PrivilegeRequest request)
    {
        Privilege entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<PrivilegeResponse>(entity);
    }

    public PrivilegeResponse RemoveModelById(int id)
    {
        Privilege entity = RemoveById(id);
        return _mapper.Map<PrivilegeResponse>(entity);
    }

    public PrivilegeResponse GetModelById(int id)
    {
        Privilege entity = GetById(id);
        return _mapper.Map<PrivilegeResponse>(entity);
    }

    public IEnumerable<PrivilegeResponse> FindAllModels(PrivilegeSearchableParam param)
    {
        IEnumerable<Privilege> entities = FindAll(param);
        return entities.Select(_mapper.Map<PrivilegeResponse>);
    }
}
