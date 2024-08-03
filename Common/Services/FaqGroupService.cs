using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using MapsterMapper;

namespace KanzApi.Common.Services;

public class FaqGroupService(IFaqGroupRepository repository, IMapper mapper)
: CrudService<FaqGroup, int?>(repository), IFaqGroupService
{

    private readonly IMapper _mapper = mapper;

    public FaqGroupResponse Add(FaqGroupRequest request)
    {
        FaqGroup entity = _mapper.Map<FaqGroup>(request);
        entity = Add(entity);

        return _mapper.Map<FaqGroupResponse>(entity);
    }

    public FaqGroupResponse Edit(int id, FaqGroupRequest request)
    {
        FaqGroup entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<FaqGroupResponse>(entity);
    }

    public FaqGroupResponse RemoveModelById(int id)
    {
        FaqGroup entity = RemoveById(id);
        return _mapper.Map<FaqGroupResponse>(entity);
    }

    public FaqGroupResponse GetModelById(int id)
    {
        FaqGroup entity = GetById(id);
        return _mapper.Map<FaqGroupResponse>(entity);
    }

    public PaginatedResponse<FaqGroupResponse> FindAllModels(FaqGroupPageableParam param)
    {
        PaginatedEntity<FaqGroup> pEntity = FindAll(param);
        IEnumerable<FaqGroupResponse> models = pEntity.Content.Select(_mapper.Map<FaqGroupResponse>);

        return PaginatedResponse<FaqGroupResponse>.From(pEntity, models);
    }
}
