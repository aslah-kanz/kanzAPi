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
public class ExchangeController(IExchangeFilterableService service) : ControllerBase
{

    private readonly IExchangeFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<ExchangeResponse> Add([FromForm] ExchangeRequest request)
    {
        ExchangeResponse data = _service.Add(request);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<ExchangeResponse> RemoveById(Guid id)
    {
        ExchangeResponse data = _service.RemoveModelById(id);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ExchangeResponse> GetById(Guid id)
    {
        ExchangeResponse data = _service.GetModelById(id);
        return ResponseMessage<ExchangeResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<ExchangeResponse>> FindAll([FromQuery] ExchangePageableParam param)
    {
        PaginatedResponse<ExchangeResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<ExchangeResponse>>.Success(data);
    }
}
