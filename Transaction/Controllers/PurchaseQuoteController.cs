using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using KanzApi.Vendors.Oto.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/purchase-quotes")]
public class PurchaseQuoteController(IPurchaseQuoteService service, IPurchaseQuoteFilterableService filterableService,
IPurchaseQuoteActionService actionService, IStoreOrderService storeOrderService) : ControllerBase
{

    private readonly IPurchaseQuoteService _service = service;

    private readonly IPurchaseQuoteFilterableService _filterableService = filterableService;

    private readonly IPurchaseQuoteActionService _actionService = actionService;

    private readonly IStoreOrderService _storeOrderService = storeOrderService;

    [HttpPost("{id}/accept")]
    public ResponseMessage<PurchaseQuoteResponse> Accept(Guid id, [FromBody] PurchaseQuoteAcceptRequest request)
    {
        PurchaseQuoteResponse data = _actionService.Accept(id, request);
        return ResponseMessage<PurchaseQuoteResponse>.Success(data);
    }

    [HttpPost("{id}/reject")]
    public ResponseMessage<PurchaseQuoteResponse> Reject(Guid id, [FromBody] PurchaseQuoteRejectRequest request)
    {
        PurchaseQuoteResponse data = _actionService.Reject(id, request);
        return ResponseMessage<PurchaseQuoteResponse>.Success(data);
    }

    [HttpPost("invoices/{invoiceNumber}/request-pickup")]
    public ResponseMessage<bool> RequestPickup(string invoiceNumber, [FromBody] RequestPickupRequest request)
    {
        _actionService.RequestPickup(invoiceNumber, (int)request.PackageCount!);
        return ResponseMessage<bool>.Success(true);
    }

    [HttpGet("{id}")]
    public ResponseMessage<PurchaseQuoteResponse> GetById(Guid id)
    {
        PurchaseQuoteResponse data = _filterableService.GetModelById(id);
        return ResponseMessage<PurchaseQuoteResponse>.Success(data);
    }

    [HttpGet("invoices/{invoiceNumber}")]
    public ResponseMessage<PurchaseQuoteInvoiceResponse> GetInvoiceByInvoiceNumber(string invoiceNumber)
    {
        PurchaseQuoteInvoiceResponse data = _filterableService.GetInvoiceByInvoiceNumber(invoiceNumber);
        return ResponseMessage<PurchaseQuoteInvoiceResponse>.Success(data);
    }

    [HttpGet("invoices/{invoiceNumber}/awb")]
    public ResponseMessage<PurchaseQuoteAwbResponse> GetAwbByInvoiceNumber(string invoiceNumber)
    {
        PurchaseQuoteAwbResponse data = _service.GetAwbByInvoiceNumber(invoiceNumber);
        return ResponseMessage<PurchaseQuoteAwbResponse>.Success(data);
    }

    [HttpGet("invoices/{invoiceNumber}/history")]
    public ResponseMessage<DeliveryHistoryResponse> FindHistoryByInvoiceNumber(string invoiceNumber)
    {
        DeliveryHistoryResponse data = _storeOrderService.FindHistoryByInvoiceNumber(invoiceNumber);
        return ResponseMessage<DeliveryHistoryResponse>.Success(data);
    }

    [HttpGet("invoices")]
    public ResponseMessage<PaginatedResponse<PurchaseQuoteInvoiceItem>> FindAllInvoices(
        [FromQuery] PurchaseQuoteInvoicePageableParam param)
    {
        PaginatedResponse<PurchaseQuoteInvoiceItem> data = _filterableService.FindAllInvoices(param);
        return ResponseMessage<PaginatedResponse<PurchaseQuoteInvoiceItem>>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<PurchaseQuoteResponse>> FindAll([FromQuery] PurchaseQuotePageableParam param)
    {
        PaginatedResponse<PurchaseQuoteResponse> data = _filterableService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<PurchaseQuoteResponse>>.Success(data);
    }
}
