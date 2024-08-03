using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class JobFieldRepository(CommonDbContext context)
: CrudRepository<JobField, int?>(context, context.JobFields), IJobFieldRepository
{ }
