using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface INotificationService : ICrudService<Notification, Guid?>
{

    NotificationResponse Map(Notification entity);

    IEnumerable<NotificationResponse>? FindAllUnreadModelsByPrincipalId(int principalId);
}
