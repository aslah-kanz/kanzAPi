using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IJobFieldService : ICrudService<JobField, int?>
{

    JobFieldResponse Add(JobFieldRequest request);

    JobFieldResponse Edit(int id, JobFieldRequest request);

    JobFieldResponse RemoveModelById(int id);

    JobFieldResponse GetModelById(int id);

    IEnumerable<JobFieldResponse> FindAllModels(JobFieldSearchableParam param);
}
