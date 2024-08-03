using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class RefundController(IRefundFilterableService service) : ControllerBase
{

    private readonly IRefundFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<RefundResponse> Add([FromForm] RefundRequest request)
    {
        RefundResponse data = _service.Add(request);
        return ResponseMessage<RefundResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<RefundResponse> RemoveById(Guid id)
    {
        RefundResponse data = _service.RemoveModelById(id);
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
