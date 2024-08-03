using System.Text.Json;
using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/orders")]
public class CustomerOrderController(ICustomerOrderService service, ICustomerOrderFilterableService filterableService,
ICustomerOrderActionService customerOrderActionService, ICustomerOrderPaymentService customerOrderPaymentService) : ControllerBase
{
    static readonly ReaderWriterLockSlim rw = new();

    private static readonly List<Tuple<int, string>> _notifications = [];

    private readonly ICustomerOrderService _service = service;

    private readonly ICustomerOrderFilterableService _filterableService = filterableService;

    private readonly ICustomerOrderActionService _customerOrderActionService = customerOrderActionService;

    private readonly ICustomerOrderPaymentService _customerOrderPaymentService = customerOrderPaymentService;

    [HttpPost("change-address")]
    public ResponseMessage<CustomerOrderCheckoutResponse> ChangeAddress([FromBody] CustomerOrderChangeAddressRequest request)
    {
        CustomerOrderCheckoutResponse data = _customerOrderActionService.ChangeAddress((int)request.Address!);
        return ResponseMessage<CustomerOrderCheckoutResponse>.Success(data);
    }

    [HttpPost("change-delivery-option")]
    public ResponseMessage<CustomerOrderCheckoutResponse> ChangeDeliveryOption([FromBody] CustomerOrderChangeDeliveryOptionRequest request)
    {
        CustomerOrderCheckoutResponse data = _service.ChangeDeliveryOption((int)request.Id!);
        return ResponseMessage<CustomerOrderCheckoutResponse>.Success(data);
    }

    [HttpPost("checkout")]
    public ResponseMessage<CustomerOrderCheckoutResponse> Checkout()
    {
        CustomerOrderCheckoutResponse data = _customerOrderActionService.Checkout();
        return ResponseMessage<CustomerOrderCheckoutResponse>.Success(data);
    }

    [HttpPost("buy-now")]
    public ResponseMessage<CustomerOrderCheckoutResponse> BuyNow([FromBody] CustomerOrderBuyNowRequest request)
    {
        CustomerOrderCheckoutResponse data = _customerOrderActionService.BuyNow(request);
        return ResponseMessage<CustomerOrderCheckoutResponse>.Success(data);
    }

    [HttpPost("pay")]
    public ResponseMessage<CustomerOrderPayResponse> Pay([FromBody] CustomerOrderPayRequest request)
    {
        CustomerOrderPayResponse data = _customerOrderPaymentService.Pay(request);
        return ResponseMessage<CustomerOrderPayResponse>.Success(data);
    }

    [HttpPost("{id}/cancel")]
    public ResponseMessage<bool> Cancel(Guid id)
    {
        _customerOrderActionService.Cancel(id);
        return ResponseMessage<bool>.Success(true);
    }

    [HttpGet("{id}")]
    public ResponseMessage<CustomerOrderResponse> GetById(Guid id)
    {
        CustomerOrderResponse data = _service.GetModelById(id);
        return ResponseMessage<CustomerOrderResponse>.Success(data);
    }

    [HttpGet("{id}/purchase-quotes")]
    public ResponseMessage<IEnumerable<CustomerOrderPurchaseQuoteResponse>> GetPurchaseQuotesByCustomerOrderId(Guid id)
    {
        var data = _service.GetPurchaseQuotesByCustomerOrderId(id);
        return ResponseMessage<IEnumerable<CustomerOrderPurchaseQuoteResponse>>.Success(data);
    }

    [HttpGet("store-orders")]
    public ResponseMessage<PaginatedResponse<CustomerOrderItem>> GetStoreOrders([FromQuery] CustomerOrderPageableParam param)
    {
        PaginatedResponse<CustomerOrderItem> data = _filterableService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<CustomerOrderItem>>.Success(data);
    }

    [HttpGet("{id}/store-orders")]
    public ResponseMessage<StoreOrderGroupResponse> GetByCustomerOrderId(Guid id)
    {
        StoreOrderGroupResponse data = _service.GetModelByIdStoreOrderGroup(id);
        return ResponseMessage<StoreOrderGroupResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<CustomerOrderItem>> FindAll([FromQuery] CustomerOrderPageableParam param)
    {
        PaginatedResponse<CustomerOrderItem> data = _filterableService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<CustomerOrderItem>>.Success(data);
    }

    [HttpGet("subscribe-pendings")]
    public async Task<IActionResult> SubscribePendingPayments(int userPrincipalId)
    {
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");

        rw.EnterReadLock();
        try
        {
            var notif = _notifications.Where(n => n.Item1 == userPrincipalId);
            if (!notif.Any())
            {
                var sseMessage = $"data: No Data \n\n";
                await Response.WriteAsync(sseMessage);
                await Response.Body.FlushAsync();
            }
            else
            {
                foreach (var notification in _notifications.Where(n => n.Item1 == userPrincipalId))
                {
                    var sseMessage = $"data: {notification.Item2}\n\n";
                    await Response.WriteAsync(sseMessage);
                    await Response.Body.FlushAsync();
                }
            }
        }
        finally
        {
            rw.ExitReadLock();
        }

        rw.EnterWriteLock();
        try
        {
            _notifications.RemoveAll(n => n.Item1 == userPrincipalId);
        }
        finally { rw.ExitWriteLock(); }

        return Ok();
    }

    private static void SendPendingPaymentsNotification(int customerPrincipalId, CustomerOrderResponse customerOrder)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
        rw.EnterWriteLock();
        try
        {
            var responseObject = new
            {
                CustomerPrincipalId = customerPrincipalId,
                CustomerOrder = customerOrder
            };
            var responseString = JsonSerializer.Serialize(responseObject, options);

            _notifications.Add(new(customerPrincipalId, responseString));
        }
        finally
        {
            rw.ExitWriteLock();
        }
    }

    // Need to be anonymous since it will posted by scheduler service
    [AllowAnonymous]
    [HttpPost("pendings")]
    public IActionResult PostPendingPayments([FromBody] PendingPaymentRequest pendingPaymentRequest)
    {
        if (pendingPaymentRequest == null || pendingPaymentRequest.CustomerOrders == null) return Ok();

        foreach (var pendingPayment in pendingPaymentRequest.CustomerOrders)
        {
            var data = _service.GetModelById(pendingPayment.CustomerOrderId);
            SendPendingPaymentsNotification(pendingPayment.CustomerPrincipalId, data);
        }

        return Ok();
    }
}
