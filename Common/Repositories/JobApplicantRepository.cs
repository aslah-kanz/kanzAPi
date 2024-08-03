using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class JobApplicantRepository(CommonDbContext context)
: CrudRepository<JobApplicant, int?>(context, context.JobApplicants), IJobApplicantRepository
{ }
