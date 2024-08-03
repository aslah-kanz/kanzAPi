using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Security.Models;
using KanzApi.Security.Services;
using KanzApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Security.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class TokenController(ITokenService service) : ControllerBase
{

    private readonly ITokenService _service = service;

    [Authorize(Policy = Privileges.GenerateToken)]
    [HttpPost("generate")]
    public ResponseMessage<TokenResponse> Generate([FromBody] TokenRequest request)
    {
        TokenResponse data = _service.Generate(request);
        return ResponseMessage<TokenResponse>.Success(data);
    }
}
