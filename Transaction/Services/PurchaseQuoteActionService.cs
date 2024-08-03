using KanzApi.Account.Services;
using KanzApi.Common.Exceptions;
using KanzApi.Extensions;
using KanzApi.Product.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using KanzApi.Vendors.Oto.Services;
using MapsterMapper;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class PurchaseQuoteActionService(IMapper mapper, IConfiguration configuration,
IPurchaseQuoteService service, IPurchaseQuoteFilterableService filterableService,
ICustomerOrderFilterableService customerOrderService, IStoreOrderService storeOrderService,
ISaleItemSyncableService saleItemService, INotificationFilterableService notificationService, IOtoOrderService otoOrderService)
: IPurchaseQuoteActionService
{

    private readonly IMapper _mapper = mapper;

    private readonly IConfiguration _configuration = configuration;

    private readonly IPurchaseQuoteService _service = service;

    private readonly IPurchaseQuoteFilterableService _filterableService = filterableService;

    private readonly ICustomerOrderFilterableService _customerOrderService = customerOrderService;

    private readonly IStoreOrderService _storeOrderService = storeOrderService;

    private readonly ISaleItemSyncableService _saleItemService = saleItemService;

    private readonly INotificationFilterableService _notificationService = notificationService;

    private readonly IOtoOrderService _otoOrderService = otoOrderService;

    private PurchaseQuote Open(PurchaseQuote entity)
    {
        if (entity.Status != EPurchaseQuoteStatus.WaitingPayment)
        {
            throw new InvalidStateChangeException(
                entity.Status.ToString()!, EPurchaseQuoteStatus.Open.ToString());
        }

        _storeOrderService.IncreaseProductCount(entity.StoreOrder!);

        entity.Status = EPurchaseQuoteStatus.Open;
        return _filterableService.Edit(entity);
    }

    public void OpenAllByCustomerOrderId(Guid customerOrderId)
    {
        using TransactionScope scope = Transactions.Create();

        IEnumerable<PurchaseQuote> entities = _service.FindAllAvailablesByCustomerOrderId(customerOrderId);
        HashSet<int> storeIds = [];
        foreach (PurchaseQuote entity in entities)
        {
            Open(entity);

            int storeId = (int)entity.StoreId!;
            if (storeIds.Contains(storeId)) continue;

            StoreOrder storeOrder = entity.StoreOrder!;
            storeOrder.PurchaseQuoteStatus = EPurchaseQuoteStatus.Open;
            _storeOrderService.Edit(storeOrder);

            _notificationService.Send(entity);

            storeIds.Add(storeId);
        }

        scope.Complete();
    }

    private void UpdateCustomerOrderStatus(CustomerOrder customerOrder, ECustomerOrderStatus status)
    {
        customerOrder.Status = status;
        _customerOrderService.Edit(customerOrder);
    }

    private void UpdateStoreOrderStatus(PurchaseQuote entity, EPurchaseQuoteStatus status)
    {
        StoreOrder storeOrder = entity.StoreOrder!;
        storeOrder.PurchaseQuoteStatus = status;
        _storeOrderService.Edit(storeOrder);
    }

    private void UpdateStoreOrderStatus(StoreOrder storeOrder, EPurchaseQuoteStatus status)
    {
        storeOrder.PurchaseQuoteStatus = status;
        _storeOrderService.Edit(storeOrder);
    }

    private void UpdateStoreOrderStatus(PurchaseQuote entity)
    {
        UpdateStoreOrderStatus(entity.StoreOrder!, (EPurchaseQuoteStatus)entity.Status!);
    }

    private void UpdateStoreOrderStatus(PurchaseQuote entity, HashSet<int> storeIds)
    {
        int storeId = (int)entity.StoreId!;
        if (storeIds.Contains(storeId)) return;

        UpdateStoreOrderStatus(entity);

        storeIds.Add(storeId);
    }

    private void CancelByAcceptance(PurchaseQuote entity, List<PurchaseQuote> entities)
    {
        entity.Status = EPurchaseQuoteStatus.CanceledByAcceptance;
        _saleItemService.Restore((long)entity.SaleItemId!, (int)entity.RequestedQuantity!);
        _filterableService.Edit(entity);

        int acceptedCount = 0, availableCount = 0;
        foreach (PurchaseQuote e in entities)
        {
            if (e.StoreOrderId != entity.StoreOrderId) continue;

            if (EPurchaseQuoteStatuses.IsAvailable((EPurchaseQuoteStatus)e.Status!)) availableCount++;
            if (e.Status == EPurchaseQuoteStatus.Accepted) acceptedCount++;
        }

        if (availableCount == 0)
        {
            UpdateStoreOrderStatus(entity);
        }
        else if (availableCount == acceptedCount)
        {
            UpdateStoreOrderStatus(entity, EPurchaseQuoteStatus.Accepted);
        }
    }

    public PurchaseQuoteResponse Accept(Guid id, PurchaseQuoteAcceptRequest request)
    {
        PurchaseQuote entity = _filterableService.GetById(id);
        bool allConfirmed = entity.TotalRequestedQuantity == request.ConfirmedQuantity;

        if (entity.Status != EPurchaseQuoteStatus.Open)
        {
            throw new InvalidStateChangeException(
                entity.Status.ToString()!, EPurchaseQuoteStatus.Accepted.ToString());
        }
        else if ((entity.RequestedQuantity == 0 || entity.RequestedQuantity != request.ConfirmedQuantity) && !allConfirmed)
        {
            throw new InvalidPurchaseQuoteConfirmedQuantityException(
                (Guid)entity.Id!, (int)entity.RequestedQuantity!, (int)entity.TotalRequestedQuantity!);
        }

        using TransactionScope scope = Transactions.Create();

        List<PurchaseQuote> entities = _service.FindAllAvailablesByCustomerOrderId((Guid)entity.CustomerOrderId!).ToList();
        bool completed = true, storeCompleted = true;
        entities.RemoveAll(e =>
        {
            if (e.Id == entity.Id) return false;
            else if (allConfirmed && e.ProductId == entity.ProductId && e.Status == EPurchaseQuoteStatus.Open)
            {
                CancelByAcceptance(e, entities);
                return true;
            }

            if (e.ProductId == entity.ProductId && e.Status == EPurchaseQuoteStatus.Open)
            {
                e.TotalRequestedQuantity -= request.ConfirmedQuantity;
                _filterableService.Edit(e);
            }

            completed &= e.Status == EPurchaseQuoteStatus.Accepted;
            if (entity.StoreId == e.StoreId) storeCompleted &= e.Status == EPurchaseQuoteStatus.Accepted;

            return false;
        });

        if (allConfirmed)
        {
            _saleItemService.Reserve((long)entity.SaleItemId!,
            (int)(entity.TotalRequestedQuantity - entity.RequestedQuantity)!, true);
        }

        if (completed)
        {
            entity.Status = EPurchaseQuoteStatus.ReadyForPacking;

            HashSet<int> storeIds = [];
            foreach (PurchaseQuote e in entities)
            {
                if (e.Id != entity.Id)
                {
                    e.Status = EPurchaseQuoteStatus.ReadyForPacking;
                    _filterableService.Edit(e);
                }

                UpdateStoreOrderStatus(e, storeIds);
            }

            UpdateCustomerOrderStatus(entity.CustomerOrder!, ECustomerOrderStatus.Packing);
        }
        else
        {
            entity.Status = EPurchaseQuoteStatus.Accepted;
            if (storeCompleted) UpdateStoreOrderStatus(entity);
        }

        decimal totalPrice = (decimal)(request.ConfirmedQuantity! * entity.Price!);
        decimal tax = _configuration.GetValue<decimal>("KanzApi:InclusiveTax");
        decimal fee = _configuration.GetValue<decimal>("KanzApi:CommissionRate");

        entity.SubTotal = (int)request.ConfirmedQuantity! * entity.OriginalPrice;
        entity.ConfirmedQuantity = request.ConfirmedQuantity;
        entity.Tax = totalPrice - totalPrice / (1 + tax);
        entity.PlatformCommission = totalPrice - entity.SubTotal - entity.Tax;

        entity = _filterableService.Edit(entity);

        PurchaseQuoteResponse model = _mapper.Map<PurchaseQuoteResponse>(entity);

        scope.Complete();

        return model;
    }

    public PurchaseQuoteResponse Reject(Guid id, PurchaseQuoteRejectRequest request)
    {
        PurchaseQuote entity = _filterableService.GetById(id);
        if (entity.Status != EPurchaseQuoteStatus.Open)
        {
            throw new InvalidStateChangeException(
                entity.Status.ToString()!, EPurchaseQuoteStatus.Rejected.ToString());
        }

        using TransactionScope scope = Transactions.Create();

        _storeOrderService.DecreaseProductCount(entity.StoreOrder!);

        List<PurchaseQuote> entities = _service.FindAllAvailablesByCustomerOrderId((Guid)entity.CustomerOrderId!).ToList();
        HashSet<int> storeIds = [];
        foreach (PurchaseQuote e in entities)
        {
            bool current = e.Id == entity.Id;
            if (current)
            {
                e.Status = EPurchaseQuoteStatus.Rejected;
                e.Comment = request.Comment;
            }
            else
            {
                e.Status = e.Status == EPurchaseQuoteStatus.Accepted
                ? EPurchaseQuoteStatus.CanceledBySystem : EPurchaseQuoteStatus.CanceledByRejection;
            }
            _filterableService.Edit(e);

            _saleItemService.Restore((long)e.SaleItemId!, (int)(e.ConfirmedQuantity ?? e.RequestedQuantity!), current);

            int storeId = (int)e.StoreId!;
            if (storeIds.Contains(storeId)) continue;

            UpdateStoreOrderStatus(e.StoreOrder!, e.StoreOrderId == entity.StoreOrderId
            ? EPurchaseQuoteStatus.Rejected : EPurchaseQuoteStatus.CanceledByRejection);

            storeIds.Add(storeId);
        }

        UpdateCustomerOrderStatus(entity.CustomerOrder!, ECustomerOrderStatus.CanceledBySystem);

        PurchaseQuoteResponse model = _mapper.Map<PurchaseQuoteResponse>(entity);

        scope.Complete();

        return model;
    }

    public void RequestPickup(string invoiceNumber, int packageCount)
    {
        StoreOrder storeOrder = _storeOrderService.GetByInvoiceNumber(invoiceNumber);
        if (storeOrder.PurchaseQuoteStatus != EPurchaseQuoteStatus.ReadyForPacking)
        {
            throw new InvalidStateChangeException(
                storeOrder.PurchaseQuoteStatus.ToString()!, EPurchaseQuoteStatus.ReadyForDelivery.ToString());
        }

        storeOrder.PackageCount = packageCount;
        storeOrder.PurchaseQuoteStatus = EPurchaseQuoteStatus.ReadyForDelivery;

        using TransactionScope scope = Transactions.Create();

        List<PurchaseQuote> entities = _service.FindAllAvailablesByStoreOrderId((Guid)storeOrder.Id!).ToList();
        foreach (PurchaseQuote entity in entities)
        {
            entity.Status = EPurchaseQuoteStatus.ReadyForDelivery;
            _filterableService.Edit(entity);

            _saleItemService.Release((long)entity.SaleItemId!, (int)entity.ConfirmedQuantity!);
        }

        CustomerOrder order = storeOrder.CustomerOrder!;

        DeliveryOption deliveryOption = order.DeliveryOption!;
        DeliveryOptionItem item = deliveryOption.Items.First(e => e.StoreId == storeOrder.StoreId);
        OtoOrderResponse response = _otoOrderService.Create(item, storeOrder, entities);

        storeOrder.DeliveryId = response.OtoId;
        _storeOrderService.Edit(storeOrder);

        UpdateCustomerOrderStatus(order, ECustomerOrderStatus.OnDelivery);

        scope.Complete();
    }

    public void UpdateAllAvailableStatusesByInvoiceNumber(string invoiceNumber, EPurchaseQuoteStatus status)
    {
        using TransactionScope scope = Transactions.Create();

        bool completed = true;
        StoreOrder storeOrder = _storeOrderService.GetByInvoiceNumber(invoiceNumber);
        IEnumerable<PurchaseQuote> purchaseQuotes = _service
        .FindAllAvailablesByCustomerOrderId((Guid)storeOrder.CustomerOrderId!);

        HashSet<int> storeIds = [];
        foreach (PurchaseQuote purchaseQuote in purchaseQuotes)
        {
            if (purchaseQuote.StoreOrderId == storeOrder.Id)
            {
                purchaseQuote.Status = status;
                _filterableService.Edit(purchaseQuote);

                UpdateStoreOrderStatus(purchaseQuote, storeIds);

                if (status == EPurchaseQuoteStatus.Delivered) _notificationService.Send(purchaseQuote);
            }
            else
            {
                completed &= purchaseQuote.Status == status;
            }
        }

        if (completed && status == EPurchaseQuoteStatus.Delivered)
        {
            UpdateCustomerOrderStatus(storeOrder.CustomerOrder!, ECustomerOrderStatus.Completed);
        }

        scope.Complete();
    }
}
