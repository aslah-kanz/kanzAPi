using KanzApi.Common.Models.Param;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models.Param;

public class StoreOrderPageableParam : PageableParam<EStoreOrderSort, StoreOrder>
{

    public StoreOrderPageableParam() : base(EStoreOrderSort.UpdatedAt) { }
}
