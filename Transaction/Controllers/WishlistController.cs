using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/wish-lists")]
public class WishListController(IWishListFilterableService service) : ControllerBase
{

    private readonly IWishListFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<WishListResponse> Add([FromBody] WishListRequest request)
    {
        WishListResponse data = _service.Add(request);
        return ResponseMessage<WishListResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<WishListResponse> Edit(int id, [FromBody] WishListRequest request)
    {
        WishListResponse data = _service.Edit(id, request);
        return ResponseMessage<WishListResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<WishListResponse> RemoveById(int id)
    {
        WishListResponse data = _service.RemoveModelById(id);
        return ResponseMessage<WishListResponse>.Success(data);
    }

    [HttpDelete("products/{productId}")]
    public ResponseMessage<WishListResponse> RemoveProductId(int productId)
    {
        WishListResponse data = _service.RemoveModelByProductId(productId);
        return ResponseMessage<WishListResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<WishListResponse> GetById(int id)
    {
        WishListResponse data = _service.GetModelById(id);
        return ResponseMessage<WishListResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<WishListResponse>> FindAll([FromQuery] WishListPageableParam param)
    {
        PaginatedResponse<WishListResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<WishListResponse>>.Success(data);
    }

    [HttpGet("product-ids")]
    public ResponseMessage<IEnumerable<int>> FindAll()
    {
        IEnumerable<int> data = _service.FindAllProductIds();
        return ResponseMessage<IEnumerable<int>>.Success(data);
    }
}
