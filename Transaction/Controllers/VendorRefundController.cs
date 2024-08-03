using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/vendor/refunds")]
public class VendorRefundController(IRefundFilterableService service) : ControllerBase
{

    private readonly IRefundFilterableService _service = service;

    [HttpPost("{id}/approve")]
    public ResponseMessage<RefundResponse> Approve(Guid id, [FromBody] VendorRefundRequest request)
    {
        RefundResponse data = _service.VendorApprove(id, request.VendorComment!);
        return ResponseMessage<RefundResponse>.Success(data);
    }

    [HttpPost("{id}/reject")]
    public ResponseMessage<RefundResponse> Reject(Guid id, [FromBody] VendorRefundRequest request)
    {
        RefundResponse data = _service.VendorReject(id, request.VendorComment!);
        return ResponseMessage<RefundResponse>.Success(data);
    }


    [HttpGet("{id}")]
    public ResponseMessage<RefundResponse> GetById(Guid id)
    {
        RefundResponse data = _service.GetModelById(id);
        return ResponseMessage<RefundResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<RefundResponse>> FindAll([FromQuery] RefundPageableParam param)
    {
        PaginatedResponse<RefundResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<RefundResponse>>.Success(data);
    }
}
