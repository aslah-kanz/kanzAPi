using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[Tags("Category")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/root-categories")]
public class RootCategoryController(ICategoryService service) : ControllerBase
{

    private readonly ICategoryService _service = service;

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<LinkedCategoryItem>> FindAll([FromQuery] CategorySearchableParam param)
    {
        IEnumerable<LinkedCategoryItem> data = _service.FindAllRootModels(param);
        return ResponseMessage<IEnumerable<LinkedCategoryItem>>.Success(data);
    }
}
