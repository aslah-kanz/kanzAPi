using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using KanzApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/admin/orders")]
public class AdminOrderController(IAdminOrderService service, IStoreOrderService storeOrderService) : ControllerBase
{

    private readonly IAdminOrderService _service = service;
    private readonly IStoreOrderService _storeOrderService = storeOrderService;

    [Authorize(Policy = Privileges.CancelOrder)]
    [HttpPost("{id}/cancel")]
    public ResponseMessage<AdminOrderResponse> Cancel(Guid id)
    {
        AdminOrderResponse data = _service.Cancel(id);
        return ResponseMessage<AdminOrderResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.CompleteOrder)]
    [HttpPost("{id}/complete")]
    public ResponseMessage<AdminOrderResponse> Complete(Guid id)
    {
        AdminOrderResponse data = _service.Complete(id);
        return ResponseMessage<AdminOrderResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.DeleteOrder)]
    [HttpDelete("{id}")]
    public ResponseMessage<AdminOrderResponse> RemoveById(Guid id)
    {
        AdminOrderResponse data = _service.RemoveModelById(id);
        return ResponseMessage<AdminOrderResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewOrder)]
    [HttpGet("{id}")]
    public ResponseMessage<AdminOrderResponse> GetById(Guid id)
    {
        AdminOrderResponse data = _service.GetModelById(id);
        return ResponseMessage<AdminOrderResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewOrder)]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<AdminOrderItem>> FindAll([FromQuery] AdminOrderPageableParam param)
    {
        PaginatedResponse<AdminOrderItem> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<AdminOrderItem>>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewOrder)]
    [HttpGet("stores/{id}")]
    public ResponseMessage<AdminStoreOrderItemDetail> GetStoreOrderById(Guid id)
    {
        AdminStoreOrderItemDetail data = _storeOrderService.GetModelById(id);
        return ResponseMessage<AdminStoreOrderItemDetail>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewOrder)]
    [HttpGet("stores")]
    public ResponseMessage<PaginatedResponse<AdminStoreOrderItem>> FindAllStoreOrders([FromQuery] StoreOrderPageableParam param)
    {
        Page page = new(param.Page, param.Size);
        PaginatedResponse<AdminStoreOrderItem> data = _storeOrderService.FindAllModels(page);
        return ResponseMessage<PaginatedResponse<AdminStoreOrderItem>>.Success(data);
    }
}
