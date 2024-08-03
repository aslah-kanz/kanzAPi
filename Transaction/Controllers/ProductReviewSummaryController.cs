using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[Tags("ProductReview")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/product-reviews/summaries")]
public class ProductReviewSummaryController(IProductReviewFilterableService service) : ControllerBase
{

    private readonly IProductReviewFilterableService _service = service;

    [HttpGet]
    public ResponseMessage<PaginatedResponse<ProductReviewSummary>> FindAll([FromQuery] ProductReviewSummaryPageableParam param)
    {
        PaginatedResponse<ProductReviewSummary> data = _service.FindAllSummaries(param);
        return ResponseMessage<PaginatedResponse<ProductReviewSummary>>.Success(data);
    }
}
