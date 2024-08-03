using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/admin/refunds")]
public class AdminRefundController(IRefundFilterableService service) : ControllerBase
{

    private readonly IRefundFilterableService _service = service;

    [HttpPost("{id}/approve")]
    public ResponseMessage<RefundResponse> Approve(Guid id, [FromBody] AdminRefundRequest request)
    {
        RefundResponse data = _service.AdminApprove(id, request.AdminComment!);
        return ResponseMessage<RefundResponse>.Success(data);
    }

    [HttpPost("{id}/reject")]
    public ResponseMessage<RefundResponse> Reject(Guid id, [FromBody] AdminRefundRequest request)
    {
        RefundResponse data = _service.AdminReject(id, request.AdminComment!);
        return ResponseMessage<RefundResponse>.Success(data);
    }


    [HttpGet("{id}")]
    public ResponseMessage<AdminRefundResponse> GetById(Guid id)
    {
        AdminRefundResponse data = _service.GetAdminModelById(id);
        return ResponseMessage<AdminRefundResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<AdminRefundResponse>> FindAll([FromQuery] AdminRefundPageableParam param)
    {
        PaginatedResponse<AdminRefundResponse> data = _service.FindAllAdminModels(param);
        return ResponseMessage<PaginatedResponse<AdminRefundResponse>>.Success(data);
    }
}
