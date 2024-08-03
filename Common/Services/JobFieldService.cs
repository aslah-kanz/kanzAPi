using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class JobFieldService(IJobFieldRepository repository, IMapper mapper)
: CrudService<JobField, int?>(repository), IJobFieldService
{

    private readonly IMapper _mapper = mapper;

    public JobFieldResponse Add(JobFieldRequest request)
    {
        JobField entity = _mapper.Map<JobField>(request);
        entity = Add(entity);

        return _mapper.Map<JobFieldResponse>(entity);
    }

    public JobFieldResponse Edit(int id, JobFieldRequest request)
    {
        JobField entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<JobFieldResponse>(entity);
    }

    public JobFieldResponse RemoveModelById(int id)
    {
        JobField entity = RemoveById(id);
        return _mapper.Map<JobFieldResponse>(entity);
    }

    public JobFieldResponse GetModelById(int id)
    {
        JobField entity = GetById(id);
        return _mapper.Map<JobFieldResponse>(entity);
    }


    public IEnumerable<JobFieldResponse> FindAllModels(JobFieldSearchableParam param)
    {
        IEnumerable<JobField> entities = FindAll(param);
        return entities.Select(_mapper.Map<JobFieldResponse>);
    }
}
