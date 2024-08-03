using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Security.Models;
using KanzApi.Security.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Security.Controllers;

[AllowAnonymous]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class AuthController(IAuthService service) : ControllerBase
{

    private readonly IAuthService _service = service;

    [HttpPost("login")]
    public ResponseMessage<AuthenticateResponse> Login([FromBody] AuthenticateRequest request)
    {
        AuthenticateResponse data = _service.Login(request);
        return ResponseMessage<AuthenticateResponse>.Success(data);
    }

    [HttpPost("signup")]
    public ResponseMessage<object?> Signup([FromBody] SignupRequest request)
    {
        _service.Signup(request);
        return ResponseMessage<object?>.Success(null);
    }

    [HttpPost("vendor/signup")]
    public ResponseMessage<object?> VendorSignup([FromBody] VendorSignupRequest request)
    {
        request.Type = Account.Entities.EPrincipalType.Vendor;

        _service.Signup(request);
        return ResponseMessage<object?>.Success(null);
    }

    [HttpPost("manufacturer/signup")]
    public ResponseMessage<object?> ManufacturerSignup([FromBody] VendorSignupRequest request)
    {
        request.Type = Account.Entities.EPrincipalType.Manufacture;

        _service.Signup(request);
        return ResponseMessage<object?>.Success(null);
    }

    [HttpPost("activate")]
    public ResponseMessage<AuthenticateResponse> Activate([FromBody] ActivateRequest request)
    {
        AuthenticateResponse data = _service.Activate(request);
        return ResponseMessage<AuthenticateResponse>.Success(data);
    }

    [HttpPost("refresh-token")]
    public ResponseMessage<TokenResponse> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        TokenResponse data = _service.RefreshToken(request);
        return ResponseMessage<TokenResponse>.Success(data);
    }


    [HttpPost("logout")]
    public ResponseMessage<object?> Logout()
    {
        _service.Logout();
        return ResponseMessage<object?>.Success(null);
    }
}
