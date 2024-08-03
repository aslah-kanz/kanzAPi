using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[Tags("ProductReview")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/products/slug/{slug}/reviews")]
public class ProductReviewBySlugController(IProductReviewService service) : ControllerBase
{

    private readonly IProductReviewService _service = service;

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<ProductReviewPaginatedResponse> FindAll(string slug, [FromQuery] ProductReviewPageableParam param)
    {
        ProductReviewPaginatedResponse data = _service.FindAllModels(slug, param);
        return ResponseMessage<ProductReviewPaginatedResponse>.Success(data);
    }
}
