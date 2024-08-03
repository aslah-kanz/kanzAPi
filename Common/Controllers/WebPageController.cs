using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/web-pages")]
public class WebPageController(IWebPageService service) : ControllerBase
{

    private readonly IWebPageService _service = service;

    [HttpPost]
    public ResponseMessage<WebPageResponse> Add([FromBody] WebPageRequest request)
    {
        WebPageResponse data = _service.Add(request);
        return ResponseMessage<WebPageResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<WebPageResponse> Edit(int id, [FromBody] WebPageRequest request)
    {
        WebPageResponse data = _service.Edit(id, request);
        return ResponseMessage<WebPageResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<WebPageResponse> RemoveById(int id)
    {
        WebPageResponse data = _service.RemoveModelById(id);
        return ResponseMessage<WebPageResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<WebPageResponse> GetById(int id)
    {
        WebPageResponse data = _service.GetModelById(id);
        return ResponseMessage<WebPageResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("slug/{slug}")]
    public ResponseMessage<WebPageResponse> GetBySlug(string slug)
    {
        WebPageResponse data = _service.GetBySlug(slug);
        return ResponseMessage<WebPageResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<WebPageResponse>> FindAll([FromQuery] WebPageSearchableParam param)
    {
        IEnumerable<WebPageResponse> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<WebPageResponse>>.Success(data);
    }
}
