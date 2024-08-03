using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IJobService : ICrudService<Job, int?>
{

    JobResponse Add(JobRequest request);

    JobResponse Edit(int id, JobRequest request);

    JobResponse RemoveModelById(int id);

    JobResponse GetModelById(int id);

    PaginatedResponse<JobResponse> FindAllModels(JobPageableParam param);
}
