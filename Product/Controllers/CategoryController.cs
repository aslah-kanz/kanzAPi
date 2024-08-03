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
[Route("v{version:apiVersion}/categories")]
public class CategoryController(ICategoryService service) : ControllerBase
{

    private readonly ICategoryService _service = service;

    [HttpPost]
    public ResponseMessage<CategoryResponse> Add([FromBody] CategoryRequest request)
    {
        CategoryResponse data = _service.Add(request);
        return ResponseMessage<CategoryResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<CategoryResponse> Edit(int id, [FromBody] CategoryRequest request)
    {
        CategoryResponse data = _service.Edit(id, request);
        return ResponseMessage<CategoryResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<CategoryResponse> RemoveById(int id)
    {
        CategoryResponse data = _service.RemoveModelById(id);
        return ResponseMessage<CategoryResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<CategoryResponse> GetById(int id)
    {
        CategoryResponse data = _service.GetModelById(id);
        return ResponseMessage<CategoryResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("slug/{slug}")]
    public ResponseMessage<CategoryResponse> GetBySlug(string slug)
    {
        CategoryResponse data = _service.GetModelBySlug(slug);
        return ResponseMessage<CategoryResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<CategoryItem>> FindAll([FromQuery] CategorySearchableParam param)
    {
        IEnumerable<CategoryItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<CategoryItem>>.Success(data);
    }
}
