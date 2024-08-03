using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/payment-methods")]
public class PaymentMethodController(IPaymentMethodService service) : ControllerBase
{

    private readonly IPaymentMethodService _service = service;

    [HttpPost]
    public ResponseMessage<PaymentMethodResponse> Add([FromBody] PaymentMethodRequest request)
    {
        PaymentMethodResponse data = _service.Add(request);
        return ResponseMessage<PaymentMethodResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<PaymentMethodResponse> Edit(int id, [FromBody] PaymentMethodRequest request)
    {
        PaymentMethodResponse data = _service.Edit(id, request);
        return ResponseMessage<PaymentMethodResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<PaymentMethodResponse> RemoveById(int id)
    {
        PaymentMethodResponse data = _service.RemoveModelById(id);
        return ResponseMessage<PaymentMethodResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<PaymentMethodResponse> GetById(int id)
    {
        PaymentMethodResponse data = _service.GetModelById(id);
        return ResponseMessage<PaymentMethodResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<IEnumerable<PaymentMethodItem>> FindAll([FromQuery] PaymentMethodSearchableParam param)
    {
        IEnumerable<PaymentMethodItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<PaymentMethodItem>>.Success(data);
    }
}
