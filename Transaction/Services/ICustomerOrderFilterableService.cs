using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface ICustomerOrderFilterableService : IFilterableCrudService<CustomerOrder, Guid?>
{

    PaginatedResponse<Models.CustomerOrderItem> FindAllModels(CustomerOrderPageableParam param);
}
