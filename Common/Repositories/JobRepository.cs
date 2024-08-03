using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class JobRepository(CommonDbContext context)
: CrudRepository<Job, int?>(context, context.Jobs), IJobRepository
{ }
