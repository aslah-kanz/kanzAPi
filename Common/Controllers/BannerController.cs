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
public class BannerController(IBannerService service) : ControllerBase
{

    private readonly IBannerService _service = service;

    [HttpPost]
    public ResponseMessage<BannerResponse> Add([FromForm] BannerRequest request)
    {
        BannerResponse data = _service.Add(request);
        return ResponseMessage<BannerResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<BannerResponse> Edit(int id, [FromForm] BannerRequest request)
    {
        BannerResponse data = _service.Edit(id, request);
        return ResponseMessage<BannerResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<BannerResponse> RemoveById(int id)
    {
        BannerResponse data = _service.RemoveModelById(id);
        return ResponseMessage<BannerResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<BannerResponse> GetById(int id)
    {
        BannerResponse data = _service.GetModelById(id);
        return ResponseMessage<BannerResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<BannerResponse>> FindAll([FromQuery] BannerPageableParam param)
    {
        PaginatedResponse<BannerResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<BannerResponse>>.Success(data);
    }
}
