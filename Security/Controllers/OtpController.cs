using Asp.Versioning;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Security.Models;
using KanzApi.Security.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Security.Controllers;

// [Authorize(Policy = Privileges.RequestOtp)]
[Tags("Auth")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/auth/otp")]
public class OtpController(IAuthService service, IOtpService otpService, IPrincipalService principalService) : ControllerBase
{

    private readonly IAuthService _service = service;
    private readonly IOtpService _otpService = otpService;

    private readonly IPrincipalService _principalService = principalService;

    [HttpPost("create")]
    public ResponseMessage<OtpResponse> Create()
    {
        var current = _principalService.GetCurrent();
        var result = _otpService.Create(current);
        OtpResponse responseResult = new OtpResponse();
        responseResult.OTP = result.Code;

        return ResponseMessage<OtpResponse>.Success(responseResult);
    }

    [HttpPost("send")]
    public ResponseMessage<OtpResponse> Send(string PhoneNumber)
    {
        var current = _principalService.GetCurrent();
        current.PhoneNumber = PhoneNumber;
        var result = _otpService.Create(current);
        OtpResponse responseResult = new OtpResponse();
        responseResult.OTP = result.Code;

        return ResponseMessage<OtpResponse>.Success(responseResult);
    }


    [HttpPost("resend")]
    public ResponseMessage<bool> Resend()
    {
        _service.ResendOtp();
        return ResponseMessage<bool>.Success(true);
    }

    [HttpPost("validate")]
    public ResponseMessage<TokenResponse> Validate([FromBody] OtpRequest request)
    {
        TokenResponse data = _service.ValidateOtp(request);
        return ResponseMessage<TokenResponse>.Success(data);
    }

    [HttpPost("validateOTP")]
    public ResponseMessage<bool> ValidateOTP([FromBody] OtpRequest request)
    {
        var data = _service.ValidateOTPCode(request);
        return ResponseMessage<bool>.Success(data);
    }
}
