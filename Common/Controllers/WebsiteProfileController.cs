using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/website-profiles")]
public class WebsiteProfileController(IWebsiteProfileService service) : ControllerBase
{

    private readonly IWebsiteProfileService _service = service;

    [HttpPost]
    public ResponseMessage<WebsiteProfileResponse> Add([FromForm] WebsiteProfileRequest request)
    {
        WebsiteProfileResponse data = _service.Add(request);
        return ResponseMessage<WebsiteProfileResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<WebsiteProfileResponse>> FindAll()
    {
        IEnumerable<WebsiteProfileResponse> data = _service.FindAllModels();
        return ResponseMessage<IEnumerable<WebsiteProfileResponse>>.Success(data);
    }
}
