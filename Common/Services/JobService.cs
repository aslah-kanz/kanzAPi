using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using MapsterMapper;

namespace KanzApi.Common.Services;

public class JobService(IJobRepository repository, IMapper mapper)
: CrudService<Job, int?>(repository), IJobService
{

    private readonly IMapper _mapper = mapper;

    public JobResponse Add(JobRequest request)
    {
        Job entity = _mapper.Map<Job>(request);
        entity = Add(entity);

        return _mapper.Map<JobResponse>(entity);
    }

    public JobResponse Edit(int id, JobRequest request)
    {
        Job entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<JobResponse>(entity);
    }

    public JobResponse RemoveModelById(int id)
    {
        Job entity = RemoveById(id);
        return _mapper.Map<JobResponse>(entity);
    }

    public JobResponse GetModelById(int id)
    {
        Job entity = GetById(id);
        return _mapper.Map<JobResponse>(entity);
    }

    public PaginatedResponse<JobResponse> FindAllModels(JobPageableParam param)
    {
        PaginatedEntity<Job> pEntity = FindAll(param);
        IEnumerable<JobResponse> models = pEntity.Content.Select(_mapper.Map<JobResponse>);

        return PaginatedResponse<JobResponse>.From(pEntity, models);
    }
}
