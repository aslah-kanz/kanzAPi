using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IJobApplicantService : ICrudService<JobApplicant, int?>
{

    JobApplicantResponse Add(int jobId, JobApplicantRequest request);

    JobApplicantResponse Approve(int id);

    JobApplicantResponse Reject(int id);

    JobApplicantResponse GetModelById(int id);

    IEnumerable<JobApplicantResponse> FindAllByJobId(int jobId);

    PaginatedResponse<JobApplicantResponse> FindAllModels(JobApplicantPageableParam param);
}
