using KanzApi.Account.Entities;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public interface INotificationRepository : ICrudRepository<Notification, Guid?> { }
