using Asp.Versioning;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class ImageController(IImageService service) : ControllerBase
{

    private readonly IImageService _service = service;

    [HttpPost]
    public ResponseMessage<ImageResponse> Add([FromForm] ImageRequest request)
    {
        ImageResponse data = _service.Add(request);
        return ResponseMessage<ImageResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<ImageResponse> Edit(long id, [FromForm] ImageRequest request)
    {
        ImageResponse data = _service.Edit(id, request);
        return ResponseMessage<ImageResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<ImageResponse> RemoveById(long id)
    {
        ImageResponse data = _service.RemoveModelById(id);
        return ResponseMessage<ImageResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ImageResponse> GetById(long id)
    {
        ImageResponse data = _service.GetModelById(id);
        return ResponseMessage<ImageResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("download/{group}/{name}")]
    public IActionResult GetByGroupAndName(EImageGroup group, string name)
    {
        Image entity = _service.GetByGroupAndName(group, name);
        Stream fs = _service.Download(entity);

        return File(fs, entity.Type!);
    }

    [AllowAnonymous]
    [HttpGet("download/{name}")]
    public IActionResult GetByGroupAndName(string name)
    {
        Image entity = _service.GetByName(name);
        Stream fs = _service.Download(entity);

        return File(fs, entity.Type!);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<ImageResponse>> FindAll([FromQuery] ImagePageableParam param)
    {
        PaginatedResponse<ImageResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<ImageResponse>>.Success(data);
    }
}
