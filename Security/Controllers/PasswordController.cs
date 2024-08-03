using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Security.Models;
using KanzApi.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Security.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/auth")]
public class PasswordController(IAuthService service) : ControllerBase
{

    private readonly IAuthService _service = service;

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public ResponseMessage<object?> Add([FromBody] ForgotPasswordRequest request)
    {
        _service.ForgotPassword(request);
        return ResponseMessage<object?>.Success(null);
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public ResponseMessage<object?> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        _service.ResetPassword(request);
        return ResponseMessage<object?>.Success(null);
    }

    [HttpPost("change-password")]
    public ResponseMessage<object?> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        _service.ChangePassword(request);
        return ResponseMessage<object?>.Success(null);
    }
}
