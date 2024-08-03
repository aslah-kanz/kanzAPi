using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Repositories;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using KanzApi.Vendors.Oto.Services;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class PurchaseQuoteService(IPurchaseQuoteRepository repository,
IStoreOrderFilterableService storeOrderService, IOtoAwbPrinterService otoAwbPrinterService)
: CrudService<PurchaseQuote, Guid?>(repository), IPurchaseQuoteService
{

    private readonly IStoreOrderFilterableService _storeOrderService = storeOrderService;

    private readonly IOtoAwbPrinterService _otoAwbPrinterService = otoAwbPrinterService;

    public int UpdateAllStatuses(IEnumerable<PurchaseQuote> entities, EPurchaseQuoteStatus status)
    {
        using TransactionScope scope = Transactions.Create();

        HashSet<int> storeIds = [];
        foreach (PurchaseQuote entity in entities)
        {
            entity.Status = status;
            Edit(entity);

            int storeId = (int)entity.StoreId!;
            if (storeIds.Contains(storeId)) continue;

            StoreOrder so = entity.StoreOrder!;
            so.PurchaseQuoteStatus = status;
            _storeOrderService.Edit(so);

            storeIds.Add(storeId);
        }

        scope.Complete();

        return entities.Count();
    }

    public int UpdateAllAvailableStatusesByCustomerOrderId(Guid customerOrderId, EPurchaseQuoteStatus status)
    {
        IEnumerable<PurchaseQuote> entities = FindAllAvailablesByCustomerOrderId(customerOrderId);
        return UpdateAllStatuses(entities, status);
    }

    public int RemoveAllByCustomerOrderId(Guid customerOrderId)
    {
        return RemoveAllByPredicate(PurchaseQuote.QCustomerOrderIdEquals(customerOrderId));
    }

    public PurchaseQuoteAwbResponse GetAwbByInvoiceNumber(string invoiceNumber)
    {
        OtoAwbPrinterResponse response = _otoAwbPrinterService.Print(invoiceNumber);
        return new()
        {
            Url = response.PrintAwbUrl,
            TrackingNumber = response.TrackingNumber
        };
    }

    public IEnumerable<PurchaseQuote> FindAllAvailablesByCustomerOrderId(Guid customerOrderId)
    {
        return FindAll(PurchaseQuote.QCustomerOrderIdEquals(customerOrderId)
        .And(PurchaseQuote.QStatusNotContains(EPurchaseQuoteStatuses.Unavailables())), null);
    }

    public IEnumerable<PurchaseQuote> FindAllByCustomerOrderId(Guid customerOrderId)
    {
        return FindAll(PurchaseQuote.QCustomerOrderIdEquals(customerOrderId), null);
    }

    public IEnumerable<PurchaseQuote> FindAllAvailablesByStoreOrderId(Guid storeOrderId)
    {
        return FindAll(PurchaseQuote.QStoreOrderIdEquals(storeOrderId)
        .And(PurchaseQuote.QStatusNotContains(EPurchaseQuoteStatuses.Unavailables())), null);
    }

    public List<Guid?> FindAllIds(Expression<Func<PurchaseQuote, bool>>? predicate)
    {
        return repository.FindAllIds(predicate);
    }
}
