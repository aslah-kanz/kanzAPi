using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Vendors.Oto.Models;

namespace KanzApi.Transaction.Services;

public interface IStoreOrderService : ICrudService<StoreOrder, Guid?>
{

    StoreOrder IncreaseProductCount(StoreOrder entity);

    StoreOrder DecreaseProductCount(StoreOrder entity);

    StoreOrder GetByInvoiceNumber(string invoiceNumber);

    DeliveryHistoryResponse FindHistoryByInvoiceNumber(string invoiceNumber);

    AdminStoreOrderItemDetail GetModelById(Guid id);

    PaginatedResponse<AdminStoreOrderItem> FindAllModels(Page page);
}


