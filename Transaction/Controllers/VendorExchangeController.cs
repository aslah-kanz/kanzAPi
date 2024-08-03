using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/vendor/exchanges")]
public class VendorExchangeController(IExchangeService service, IExchangeFilterableService filterableService)
: ControllerBase
{

    private readonly IExchangeService _service = service;

    private readonly IExchangeFilterableService _filterableService = filterableService;

    [HttpPost("{id}/approve")]
    public ResponseMessage<ExchangeResponse> Approve(Guid id, [FromBody] VendorExchangeRequest request)
    {
        ExchangeResponse data = _service.VendorApprove(id, request.VendorComment!);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpPost("{id}/reject")]
    public ResponseMessage<ExchangeResponse> Reject(Guid id, [FromBody] VendorExchangeRequest request)
    {
        ExchangeResponse data = _service.VendorReject(id, request.VendorComment!);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ExchangeResponse> GetById(Guid id)
    {
        ExchangeResponse data = _filterableService.GetModelById(id);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<ExchangeResponse>> FindAll([FromQuery] ExchangePageableParam param)
    {
        PaginatedResponse<ExchangeResponse> data = _filterableService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<ExchangeResponse>>.Success(data);
    }
}
