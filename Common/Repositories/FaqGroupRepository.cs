using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class FaqGroupRepository(CommonDbContext context)
: CrudRepository<FaqGroup, int?>(context, context.FaqGroups), IFaqGroupRepository
{ }
