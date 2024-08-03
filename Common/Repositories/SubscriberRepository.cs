using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class SubscriberRepository(CommonDbContext context)
: CrudRepository<Subscriber, int?>(context, context.Subscribers), ISubscriberRepository
{ }
