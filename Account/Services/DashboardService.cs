using KanzApi.Account.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Product.Services;
using KanzApi.Transaction.Services;

namespace KanzApi.Account.Services;

public class DashboardService(ISessionService sessionService, ISaleItemSellableService saleItemService,
IPurchaseQuoteService purchaseQuoteService, IStoreOrderService storeOrderService, IStoreFilterableService storeService) : IDashboardService
{
    private readonly ISessionService _sessionService = sessionService;
    private readonly ISaleItemSellableService _saleItemService = saleItemService;
    private readonly IStoreOrderService _storeOrderService = storeOrderService;
    private readonly IPurchaseQuoteService purchaseQuoteService = purchaseQuoteService;
    private readonly IStoreFilterableService _storeService = storeService;

    public VendorDashboardResponse GetVendorDashboard()
    {

        var principal = _sessionService.CurrentPrincipalId();
        var storeIds = _storeService.FindAll().Select(e => e.Id).ToList();
        var saleItems = _saleItemService.FindAllProducts(principal!.Value).Count();
        var storeOrders = _storeOrderService.FindAll(e => storeIds.Contains(e.StoreId!.Value), null).Count();
        var purchaseQuotes = purchaseQuoteService.FindAll(e => storeIds.Contains(e.StoreId), null).Count();

        VendorDashboardResponse response = new()
        {
            TotalOrder = purchaseQuotes,
            TotalProducts = saleItems,
            TotalTransactions = storeOrders
        };

        return response;
    }
}
