using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class ProductController(IProductSyncableService service, IProductSaleItemFilterableService productSaleItemService) : ControllerBase
{

    private readonly IProductSyncableService _service = service;

    private readonly IProductSaleItemFilterableService _productSaleItemService = productSaleItemService;

    [HttpPost]
    public ResponseMessage<ProductResponse> Add([FromBody] ProductRequest request)
    {
        ProductResponse data = _service.Add(request);
        return ResponseMessage<ProductResponse>.Success(data);
    }

    [HttpPut("{id}/status")]
    public ResponseMessage<ProductResponse> ChangeStatus(int id, [FromBody] ProductStatusRequest request)
    {
        ProductResponse data = _service.ChangeStatus(id, request);
        return ResponseMessage<ProductResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<ProductResponse> Edit(int id, [FromBody] ProductRequest request)
    {
        ProductResponse data = _service.Edit(id, request);
        return ResponseMessage<ProductResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<ProductResponse> RemoveById(int id)
    {
        ProductResponse data = _service.RemoveModelById(id);
        return ResponseMessage<ProductResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ProductResponse> GetById(int id)
    {
        ProductResponse data = _service.GetModelById(id);
        return ResponseMessage<ProductResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<ProductItem>> FindAll([FromQuery] ProductPageableParam param)
    {
        PaginatedResponse<ProductItem> data = _productSaleItemService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<ProductItem>>.Success(data);
    }
}
