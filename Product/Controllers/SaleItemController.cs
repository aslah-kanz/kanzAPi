using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/sale-items")]
public class SaleItemController(ISaleItemFilterableService service) : ControllerBase
{

    private readonly ISaleItemFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<SaleItemResponse> Add([FromBody] SaleItemRequest request)
    {
        SaleItemResponse data = _service.Add(request);
        return ResponseMessage<SaleItemResponse>.Success(data);
    }

    [HttpPut("{id}/enabled")]
    public ResponseMessage<SaleItemResponse> ChangeEnabled(long id, [FromBody] SaleItemStatusRequest request)
    {
        SaleItemResponse data = _service.ChangeEnabled(id, (bool)request.Enabled!);
        return ResponseMessage<SaleItemResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<SaleItemResponse> Edit(long id, [FromBody] SaleItemRequest request)
    {
        SaleItemResponse data = _service.Edit(id, request);
        return ResponseMessage<SaleItemResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<SaleItemResponse> RemoveById(long id)
    {
        SaleItemResponse data = _service.RemoveModelById(id);
        return ResponseMessage<SaleItemResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<SaleItemResponse> GetById(long id)
    {
        SaleItemResponse data = _service.GetModelById(id);
        return ResponseMessage<SaleItemResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<SaleItemResponse>> FindAll([FromQuery] SaleItemPageableParam param)
    {
        PaginatedResponse<SaleItemResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<SaleItemResponse>>.Success(data);
    }

    [HttpGet("products/{productId}")]
    public ResponseMessage<PaginatedResponse<SaleItemStoreResponse>> FindAll(int productId, [FromQuery] SaleItemPageableParam param)
    {
        PaginatedResponse<SaleItemStoreResponse> data = _service.FindAllModels(productId, param);
        return ResponseMessage<PaginatedResponse<SaleItemStoreResponse>>.Success(data);
    }
}
