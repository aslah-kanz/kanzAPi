using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class BlogController(IBlogService service) : ControllerBase
{

    private readonly IBlogService _service = service;

    [HttpPost]
    public ResponseMessage<BlogResponse> Add([FromBody] BlogRequest request)
    {
        BlogResponse data = _service.Add(request);
        return ResponseMessage<BlogResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<BlogResponse> Edit(int id, [FromBody] BlogRequest request)
    {
        BlogResponse data = _service.Edit(id, request);
        return ResponseMessage<BlogResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<BlogResponse> RemoveById(int id)
    {
        BlogResponse data = _service.RemoveModelById(id);
        return ResponseMessage<BlogResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<BlogResponse> GetById(int id)
    {
        BlogResponse data = _service.GetModelById(id);
        return ResponseMessage<BlogResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<BlogResponse>> FindAll([FromQuery] BlogPageableParam param)
    {
        PaginatedResponse<BlogResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<BlogResponse>>.Success(data);
    }
}
