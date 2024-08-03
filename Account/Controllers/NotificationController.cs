using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class NotificationController(INotificationService service, INotificationFilterableService filterableService) : ControllerBase
{

    private readonly INotificationService _service = service;

    private readonly INotificationFilterableService _filterableService = filterableService;

    [HttpPost("{id}/read")]
    public ResponseMessage<bool> Read(Guid id)
    {
        _filterableService.Read(id);
        return ResponseMessage<bool>.Success(true);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<NotificationResponse>> FindAll([FromQuery] NotificationPageableParam param)
    {
        PaginatedResponse<NotificationResponse> data = _filterableService.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<NotificationResponse>>.Success(data);
    }

    [HttpGet("subscribe-vendor")]
    public async Task<IActionResult> SubscribeVendorNotification(int vendorPrincipalId)
    {
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("Connection", "keep-alive");

        var notifications = _service
            .FindAllUnreadModelsByPrincipalId(vendorPrincipalId);

        if (!notifications!.Any())
        {
            var sseNoData = $"data: No Data \n\n";
            await Response.WriteAsync(sseNoData);
            await Response.Body.FlushAsync();

            return Ok();
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
        
        var message = JsonSerializer.Serialize(notifications, options);

        var sseMessage = $"data: {message}\n\n";
        await Response.WriteAsync(sseMessage);
        await Response.Body.FlushAsync();

        return Ok();
    }
}
