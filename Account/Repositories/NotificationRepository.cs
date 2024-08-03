using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class NotificationRepository(CommonDbContext context)
: CrudRepository<Notification, Guid?>(context, context.Notifications), INotificationRepository
{ }
