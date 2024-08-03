using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[Tags("Product")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class OverviewController(IProductSyncableService service) : ControllerBase
{

    private readonly IProductSyncableService _service = service;

    [AllowAnonymous]
    [HttpGet("product-categories")]
    public ResponseMessage<IEnumerable<OverviewCategoryItem>> FindAll()
    {
        IEnumerable<OverviewCategoryItem> data = _service.FindAllOverviewCategoryModels();
        return ResponseMessage<IEnumerable<OverviewCategoryItem>>.Success(data);
    }
}
