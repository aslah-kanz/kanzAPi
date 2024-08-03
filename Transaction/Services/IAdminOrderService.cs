using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IAdminOrderService : ICrudService<CustomerOrder, Guid?>
{

    AdminOrderResponse Cancel(Guid id);

    AdminOrderResponse Complete(Guid id);

    AdminOrderResponse RemoveModelById(Guid id);

    AdminOrderResponse GetModelById(Guid id);

    PaginatedResponse<AdminOrderItem> FindAllModels(AdminOrderPageableParam param);
}
