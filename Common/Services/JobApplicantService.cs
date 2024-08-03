using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class JobApplicantService(IJobApplicantRepository repository,
IMapper mapper, IJobService jobService, IDocumentService documentService)
: CrudService<JobApplicant, int?>(repository), IJobApplicantService
{

    private readonly IMapper _mapper = mapper;

    private readonly IJobService _jobService = jobService;

    private readonly IDocumentService _documentService = documentService;

    public JobApplicantResponse Add(int jobId, JobApplicantRequest request)
    {
        JobApplicant entity = _mapper.Map<JobApplicant>(request);

        Job job = _jobService.GetById(jobId);
        entity.Job = job;

        if (request.File != null)
        {
            Document document = _documentService.AddWithRandomName(request.File);
            entity.Document = document;
        }

        entity = Add(entity);

        return _mapper.Map<JobApplicantResponse>(entity);
    }

    public JobApplicantResponse Approve(int id)
    {
        JobApplicant entity = GetById(id);
        entity.Status = EJobApplicantStatus.Approved;
        entity = Edit(entity);

        return _mapper.Map<JobApplicantResponse>(entity);
    }

    public JobApplicantResponse Reject(int id)
    {
        JobApplicant entity = GetById(id);
        entity.Status = EJobApplicantStatus.Rejected;
        entity = Edit(entity);

        return _mapper.Map<JobApplicantResponse>(entity);
    }

    public JobApplicantResponse GetModelById(int id)
    {
        JobApplicant entity = GetById(id);
        return _mapper.Map<JobApplicantResponse>(entity);
    }

    public IEnumerable<JobApplicantResponse> FindAllByJobId(int jobId)
    {
        IEnumerable<JobApplicant> jobApplicants = FindAll(JobApplicant.QJobIdEquals(jobId), null);
        return jobApplicants.Select(_mapper.Map<JobApplicantResponse>);
    }

    public PaginatedResponse<JobApplicantResponse> FindAllModels(JobApplicantPageableParam param)
    {
        PaginatedEntity<JobApplicant> pEntity = FindAll(param);
        IEnumerable<JobApplicantResponse> models = pEntity.Content.Select(_mapper.Map<JobApplicantResponse>);

        return PaginatedResponse<JobApplicantResponse>.From(pEntity, models);
    }
}
