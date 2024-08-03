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
public class CartController(ICartFilterableService service) : ControllerBase
{

    private readonly ICartFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<CartResponse> Add([FromBody] AddCartRequest request)
    {
        CartResponse data = _service.Add(request);
        return ResponseMessage<CartResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<CartResponse> Edit(int id, [FromBody] EditCartRequest request)
    {
        CartResponse data = _service.Edit(id, request);
        return ResponseMessage<CartResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<CartResponse> RemoveById(int id)
    {
        CartResponse data = _service.RemoveModelById(id);
        return ResponseMessage<CartResponse>.Success(data);
    }

    [HttpDelete("products")]
    public ResponseMessage<int> RemoveByProductIds([FromQuery] CartProductParam param)
    {
        int data = _service.RemoveAllByProductIds(param.Ids);
        return ResponseMessage<int>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<CartResponse> GetById(int id)
    {
        CartResponse data = _service.GetModelById(id);
        return ResponseMessage<CartResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<CartsResponse> FindAll()
    {
        CartsResponse data = _service.FindAllModels();
        return ResponseMessage<CartsResponse>.Success(data);
    }
}
