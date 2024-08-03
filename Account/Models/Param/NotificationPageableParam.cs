using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;

namespace KanzApi.Account.Models.Param;

public class NotificationPageableParam()
: PageableParam<ENotificationSort, Notification>(ENotificationSort.UpdatedAt)
{ }
