using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;

namespace KanzApi.Account.Services;

public interface INotificationFilterableService : IFilterableCrudService<Notification, Guid?>
{

    void Send(Principal principal, IEnumerable<Principal> approvers);

    Notification Send(PurchaseQuote purchaseQuote);

    Notification Read(Guid id);

    PaginatedResponse<NotificationResponse> FindAllModels(NotificationPageableParam param);
}
