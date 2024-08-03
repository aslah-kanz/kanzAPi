using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/shipping-methods")]
public class ShippingMethodController(IShippingMethodService service) : ControllerBase
{

    private readonly IShippingMethodService _service = service;

    [HttpPost]
    public ResponseMessage<ShippingMethodResponse> Add([FromBody] ShippingMethodRequest request)
    {
        ShippingMethodResponse data = _service.Add(request);
        return ResponseMessage<ShippingMethodResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<ShippingMethodResponse> Edit(int id, [FromBody] ShippingMethodRequest request)
    {
        ShippingMethodResponse data = _service.Edit(id, request);
        return ResponseMessage<ShippingMethodResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<ShippingMethodResponse> RemoveById(int id)
    {
        ShippingMethodResponse data = _service.RemoveModelById(id);
        return ResponseMessage<ShippingMethodResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ShippingMethodResponse> GetById(int id)
    {
        ShippingMethodResponse data = _service.GetModelById(id);
        return ResponseMessage<ShippingMethodResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<IEnumerable<ShippingMethodItem>> FindAll([FromQuery] ShippingMethodSearchableParam param)
    {
        IEnumerable<ShippingMethodItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<ShippingMethodItem>>.Success(data);
    }
}
