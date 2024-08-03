using Asp.Versioning;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class AccountController(IPrincipalService service) : ControllerBase
{

    private readonly IPrincipalService _service = service;

    [HttpPost("request-approval")]
    public ResponseMessage<bool> RequestApproval()
    {
        _service.RequestApproval();
        return ResponseMessage<bool>.Success(true);
    }
}
