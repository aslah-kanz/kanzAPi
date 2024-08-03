using KanzApi.Account.Services;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Repositories;
using KanzApi.Utils;
using MapsterMapper;

namespace KanzApi.Transaction.Services;

public class CustomerOrderService(ICustomerOrderRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
IPrincipalDetailFilterableService principalDetailService, IPrincipalAddressFilterableService principalAddressService,
IPurchaseQuoteService purchaseQuoteService, CommonDbContext context)
: CrudService<CustomerOrder, Guid?>(repository), ICustomerOrderService
{

    private readonly CommonDbContext _context = context;
    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    private readonly IPrincipalAddressFilterableService _principalAddressService = principalAddressService;

    private readonly IPurchaseQuoteService _purchaseQuoteService = purchaseQuoteService;

    public void SetDeliveryOption(CustomerOrder entity, int? id)
    {
        DeliveryOption? deliveryOption = id != null
        ? entity.DeliveryOptions!.FirstOrDefault(e => e.Id == id)
        ?? throw EntityNotFoundException.From(typeof(DeliveryOption), "ID", id)
        : null;

        entity.DeliveryOptionId = deliveryOption?.Id;
        entity.EstimatedDeliveryCost = deliveryOption?.EstimatedCost ?? 0;
        entity.GrandTotal = entity.SubTotal + entity.EstimatedDeliveryCost - entity.DiscountPrice ?? 0;
    }

    public CustomerOrderCheckoutResponse ChangeDeliveryOption(int id)
    {
        CustomerOrder entity = GetCurrent();
        SetDeliveryOption(entity, id);

        entity = Edit(entity);

        CustomerOrderCheckoutResponse response = _mapper.Map<CustomerOrderCheckoutResponse>(entity);

        return response;
    }

    public override CustomerOrder Remove(CustomerOrder entity)
    {
        _purchaseQuoteService.RemoveAllByCustomerOrderId((Guid)entity.Id!);
        return base.Remove(entity);
    }

    public CustomerOrder? FindCurrent()
    {
        return FindByPredicate(
            CustomerOrder.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!)
            .And(CustomerOrder.QStatusEquals(ECustomerOrderStatus.Open)));
    }

    public CustomerOrder GetCurrent()
    {
        return FindCurrent()
        ?? throw EntityNotFoundException.From(typeof(CustomerOrder), "Principal ID", (int)_sessionService.CurrentPrincipalId()!);
    }

    public CustomerOrder Create()
    {
        RemoveAllByPredicate(CustomerOrder.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!)
        .And(CustomerOrder.QStatusContains(ECustomerOrderStatus.Open)));

        return new()
        {
            Principal = _principalService.GetCurrent(),
            PrincipalDetail = _principalDetailService.FindByCurrentPrincipal(),
            Address = _principalAddressService.GetDefault()
        }; ;
    }

    public CustomerOrder GetByPaymentTrackId(string paymentTrackId)
    {
        return FindByPredicate(CustomerOrder.QPaymentTrackIdEquals(paymentTrackId))
        ?? throw EntityNotFoundException.From(typeof(CustomerOrder), "Payment Track ID", paymentTrackId);
    }

    public CustomerOrderResponse GetModelById(Guid id)
    {
        CustomerOrder entity = GetById(id);
        CustomerOrderResponse response = _mapper.Map<CustomerOrderResponse>(entity);

        var deliveryDetail = entity.DeliveryOptions != null ? entity.DeliveryOptions.FirstOrDefault(x => x.Id == entity.DeliveryOptionId) : null;
        response.EstimatedDeliveryDay = deliveryDetail != null ? deliveryDetail.MaxEstimatedDay > 1 ? $"{deliveryDetail.MinEstimatedDay} to {deliveryDetail.MaxEstimatedDay} Days" : $"{deliveryDetail.MaxEstimatedDay} Day" : "";
        return response;
    }

    public IEnumerable<CustomerOrderPurchaseQuoteResponse> GetPurchaseQuotesByCustomerOrderId(Guid id)
    {
        var purchaseQuotes = _purchaseQuoteService.FindAllByCustomerOrderId(id);
        return purchaseQuotes.Select(_mapper.Map<CustomerOrderPurchaseQuoteResponse>);
    }

    public StoreOrderGroupResponse GetModelByIdStoreOrderGroup(Guid id)
    {
        var entity = GetById(id);
        var model = _mapper.Map<StoreOrderGroupResponse>(entity);

        var purchaseQuotes = _purchaseQuoteService.FindAllByCustomerOrderId(id);
        var customerOrderItems = new List<PurchaseQuoteByStoreOrderResponse>();

        foreach (var purchaseQuote in purchaseQuotes)
        {
            if (purchaseQuote.Status == EPurchaseQuoteStatus.Unassigned) continue;
            var isRefunded = _context.Refunds.Any(refund => refund.PurchaseQuoteId == purchaseQuote.Id);
            var isExchanged = _context.Exchanges.Any(exchange => exchange.PurchaseQuoteId == purchaseQuote.Id);
            var map = _mapper.Map<PurchaseQuoteByStoreOrderResponse>(purchaseQuote);
            map.IsReviewed = _context.ProductReviews.Any(x => x.PurchaseQuoteId == purchaseQuote.Id);
            map.IsExchangeable = purchaseQuote.Status == EPurchaseQuoteStatus.Delivered && DateUtils.SetIsExchangeableIsRefundable(purchaseQuote.UpdatedAt) && !isRefunded;
            map.IsRefundable = purchaseQuote.Status == EPurchaseQuoteStatus.Delivered && DateUtils.SetIsExchangeableIsRefundable(purchaseQuote.UpdatedAt) && !isExchanged;
            map.IsRefunded = isRefunded;
            map.IsExchanged = isExchanged;
            customerOrderItems.Add(map);
        }
        model.PurchaseQuotes = customerOrderItems;
        return model;
    }
}