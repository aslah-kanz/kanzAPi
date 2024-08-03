using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class BrandController(IBrandService service) : ControllerBase
{

    private readonly IBrandService _service = service;

    [HttpPost]
    public ResponseMessage<BrandResponse> Add([FromBody] BrandRequest request)
    {
        BrandResponse data = _service.Add(request);
        return ResponseMessage<BrandResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<BrandResponse> Edit(int id, [FromBody] BrandRequest request)
    {
        BrandResponse data = _service.Edit(id, request);
        return ResponseMessage<BrandResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<BrandResponse> RemoveById(int id)
    {
        BrandResponse data = _service.RemoveModelById(id);
        return ResponseMessage<BrandResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<BrandResponse> GetById(int id)
    {
        BrandResponse data = _service.GetModelById(id);
        return ResponseMessage<BrandResponse>.Success(data);
    }

    [HttpGet("slug/{slug}")]
    public ResponseMessage<BrandResponse> GetBySlug(string slug)
    {
        BrandResponse data = _service.GetModelBySlug(slug);
        return ResponseMessage<BrandResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<BrandItem>> FindAll([FromQuery] BrandPageableParam param)
    {
        PaginatedResponse<BrandItem> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<BrandItem>>.Success(data);
    }
}
