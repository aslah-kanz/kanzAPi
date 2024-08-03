using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/admin/exchanges")]
public class AdminExchangeController(IExchangeService service) : ControllerBase
{

    private readonly IExchangeService _service = service;

    [HttpPost("{id}/approve")]
    public ResponseMessage<ExchangeResponse> Approve(Guid id, [FromBody] AdminExchangeRequest request)
    {
        ExchangeResponse data = _service.AdminApprove(id, request.AdminComment!);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpPost("{id}/reject")]
    public ResponseMessage<ExchangeResponse> Reject(Guid id, [FromBody] AdminExchangeRequest request)
    {
        ExchangeResponse data = _service.AdminReject(id, request.AdminComment!);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<AdminExchangeResponse> GetById(Guid id)
    {
        AdminExchangeResponse data = _service.GetAdminModelById(id);
        return ResponseMessage<AdminExchangeResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<AdminExchangeResponse>> FindAll([FromQuery] AdminExchangePageableParam param)
    {
        PaginatedResponse<AdminExchangeResponse> data = _service.FindAllAdminModels(param);
        return ResponseMessage<PaginatedResponse<AdminExchangeResponse>>.Success(data);
    }
}
