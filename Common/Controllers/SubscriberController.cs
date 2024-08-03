using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[AllowAnonymous]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class SubscriberController(ISubscriberService service) : ControllerBase
{

    private readonly ISubscriberService _service = service;

    [HttpPost]
    public ResponseMessage<SubscriberResponse> Add([FromBody] SubscriberRequest request)
    {
        SubscriberResponse data = _service.Add(request);
        return ResponseMessage<SubscriberResponse>.Success(data);
    }
}
