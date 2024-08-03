using KanzApi.Common.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Messaging.Services;
using KanzApi.Product.Services;
using KanzApi.Security.Entities;
using KanzApi.Security.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Utils;
using KanzApi.Vendors.Urway.Models;
using KanzApi.Vendors.Urway.Services;
using MapsterMapper;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class CustomerOrderPaymentService(ICustomerOrderService service, IMapper mapper, ICartService cartService,
IPurchaseQuoteService purchaseQuoteService, IPurchaseQuoteActionService purchaseQuoteActionService,
IPaymentMethodService paymentMethodService, IOneTimeTokenService oneTimeTokenService,
IUrwayTransactionService urwayTransactionService, ISaleItemSyncableService saleItemService, IMailService mailService)
: ICustomerOrderPaymentService
{

    private readonly ICustomerOrderService _service = service;

    private readonly IMapper _mapper = mapper;

    private readonly ICartService _cartService = cartService;

    private readonly IPurchaseQuoteService _purchaseQuoteService = purchaseQuoteService;

    private readonly IPurchaseQuoteActionService _purchaseQuoteActionService = purchaseQuoteActionService;

    private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;

    private readonly IOneTimeTokenService _oneTimeTokenService = oneTimeTokenService;

    private readonly IUrwayTransactionService _urwayTransactionService = urwayTransactionService;

    private readonly ISaleItemSyncableService _saleItemService = saleItemService;

    private readonly IMailService _mailService = mailService;

    public CustomerOrderPayResponse Pay(CustomerOrderPayRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        CustomerOrder entity = _service.GetCurrent();
        entity.Status = ECustomerOrderStatus.InPayment;

        IEnumerable<PurchaseQuote> purchaseQuotes = _purchaseQuoteService.FindAllAvailablesByCustomerOrderId((Guid)entity.Id!);
        foreach (PurchaseQuote purchaseQuote in purchaseQuotes)
        {
            _saleItemService.Reserve((long)purchaseQuote.SaleItemId!, (int)purchaseQuote.RequestedQuantity!);
        }

        PaymentMethod paymentMethod = _paymentMethodService.GetById(request.PaymentMethod);
        entity.PaymentMethod = paymentMethod;

        if (request.DeliveryOptionId != null) _service.SetDeliveryOption(entity, (int)request.DeliveryOptionId);

        OneTimeToken token = _oneTimeTokenService.Create(entity.Principal!, EOneTimeTokenType.Payment);

        UrwayTransactionResponse transactionResponse = _urwayTransactionService.Send(entity, token, request.RedirectPath);

        _service.Edit(entity);

        CustomerOrderPayResponse response = _mapper.Map<CustomerOrderPayResponse>(entity);
        response.Url = transactionResponse.RedirectUrl;

        scope.Complete();

        return response;
    }

    public void PayCallback(UrwayWebHookRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        OneTimeToken token = _oneTimeTokenService.Validate(
            request.MetaData!.Token!, EOneTimeTokenType.Payment);

        CustomerOrder entity = _service.GetByPaymentTrackId(request.TrackId!);
        Guid id = (Guid)entity.Id!;
        entity.PaymentTransactionId = request.TransactionId;

        if (request.IsSuccess())
        {
            if (entity.Status != ECustomerOrderStatus.InPayment)
            {
                throw new InvalidStateChangeException(
                    entity.Status.ToString()!, ECustomerOrderStatus.Paid.ToString());
            }
            entity.Status = ECustomerOrderStatus.Paid;

            _cartService.Clear((int)entity.PrincipalId!);
            _purchaseQuoteActionService.OpenAllByCustomerOrderId(id);

            _mailService.Send(entity);
        }
        else
        {
            if (entity.Status != ECustomerOrderStatus.InPayment)
            {
                throw new InvalidStateChangeException(
                    entity.Status.ToString()!, ECustomerOrderStatus.Failed.ToString());
            }
            entity.Status = ECustomerOrderStatus.Failed;

            Dictionary<long, int> map = [];
            IEnumerable<PurchaseQuote> purchaseQuotes = _purchaseQuoteService.FindAllAvailablesByCustomerOrderId((Guid)entity.Id!);
            foreach (PurchaseQuote purchaseQuote in purchaseQuotes)
            {
                long saleItemId = (long)purchaseQuote.SaleItemId!;
                map.TryGetValue(saleItemId, out int quantity);
                quantity += (int)purchaseQuote.RequestedQuantity!;

                map[saleItemId] = quantity;
            }

            foreach (KeyValuePair<long, int> pair in map)
            {
                _saleItemService.Restore(pair.Key, pair.Value);
            }

            _purchaseQuoteService.UpdateAllStatuses(purchaseQuotes, EPurchaseQuoteStatus.Failed);
        }

        _service.Edit(entity);

        _oneTimeTokenService.Remove(token);

        scope.Complete();
    }
}
