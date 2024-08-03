using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/stores/{storeId}/products/{productId}/reviews")]
public class StoreProductReviewController(IProductReviewService service,
IProductReviewFilterableService filterableService) : ControllerBase
{

    private readonly IProductReviewService _service = service;

    private readonly IProductReviewFilterableService _filterableService = filterableService;

    [HttpGet]
    public ResponseMessage<PaginatedResponse<ProductReviewItem>> FindAll(int storeId, int productId, [FromQuery] ProductReviewPageableParam param)
    {
        PaginatedResponse<ProductReviewItem> data = _service.FindAllModels(storeId, productId, param);
        return ResponseMessage<PaginatedResponse<ProductReviewItem>>.Success(data);
    }

    [HttpGet("summary")]
    public ResponseMessage<ProductReviewRatingsSummary?> FindSummary(int storeId, int productId)
    {
        ProductReviewRatingsSummary? data = _filterableService.FindSummary(storeId, productId);
        return ResponseMessage<ProductReviewRatingsSummary?>.Success(data);
    }

	[HttpGet("detail")]
	public ResponseMessage<ProductReviewRatingDetailResponse?> FindDetail(int storeId, int productId, [FromQuery] ProductReviewDetailPageableParam param)
	{
		ProductReviewRatingDetailResponse? data = _filterableService.GetDetail(storeId, productId, param);
		return ResponseMessage<ProductReviewRatingDetailResponse?>.Success(data);
	}
}
