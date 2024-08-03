using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Services;
using KanzApi.Utils;
using KanzApi.Vendors.Oto.Models;
using KanzApi.Vendors.Urway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[AllowAnonymous]
[Tags("CustomerOrder")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/callback/orders")]
public class CustomerOrderCallbackController(
    ICustomerOrderPaymentService customerOrderPaymentService,
    IPurchaseQuoteActionService purchaseQuoteActionService)
: ControllerBase
{

    private readonly ICustomerOrderPaymentService _customerOrderPaymentService = customerOrderPaymentService;

    private readonly IPurchaseQuoteActionService _purchaseQuoteActionService = purchaseQuoteActionService;

    [HttpPost("pay")]
    public ResponseMessage<bool> Pay([FromBody] UrwayWebHookRequest request)
    {
        _customerOrderPaymentService.PayCallback(request);
        return ResponseMessage<bool>.Success(true);
    }

    [Authorize(Policy = Privileges.DeliverCallback)]
    [HttpPost("deliver")]
    public ResponseMessage<bool> Deliver([FromBody] OtoWebHookRequest request)
    {
        EPurchaseQuoteStatus status = Enum.Parse<EPurchaseQuoteStatus>(request.Status!.ToUpperFirstChar());
        _purchaseQuoteActionService.UpdateAllAvailableStatusesByInvoiceNumber(request.OrderId!, status);
        return ResponseMessage<bool>.Success(true);
    }
}
