using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/products/{productId}/images")]
public class ProductImageController(IProductImageService service) : ControllerBase
{

    private readonly IProductImageService _service = service;

    [HttpPost]
    public ResponseMessage<ProductImageResponse> Add(int productId, [FromBody] ProductImageRequest request)
    {
        ProductImageResponse data = _service.Add(productId, request);
        return ResponseMessage<ProductImageResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<ProductImageResponse> Edit(int productId, int id, [FromBody] ProductImageRequest request)
    {
        ProductImageResponse data = _service.Edit(productId, id, request);
        return ResponseMessage<ProductImageResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<ProductImageResponse> RemoveById(int productId, int id)
    {
        ProductImageResponse data = _service.RemoveModelById(productId, id);
        return ResponseMessage<ProductImageResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ProductImageResponse> GetById(int productId, int id)
    {
        ProductImageResponse data = _service.GetModelById(productId, id);
        return ResponseMessage<ProductImageResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<IEnumerable<ProductImageResponse>> FindAll(int productId,
    [FromQuery] ProductImageSortableParam param)
    {
        IEnumerable<ProductImageResponse> data = _service.FindAllModels(productId, param);
        return ResponseMessage<IEnumerable<ProductImageResponse>>.Success(data);
    }
}
