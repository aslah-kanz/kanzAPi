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
[Route("v{version:apiVersion}/purchase-quotes/{purchaseQuoteId}/reviews")]
public class PurchaseQuoteReviewController(IProductReviewService service,
IProductReviewFilterableService filterableService) : ControllerBase
{

    private readonly IProductReviewService _service = service;

    private readonly IProductReviewFilterableService _filterableService = filterableService;

    [HttpPost]
    public ResponseMessage<ProductReviewResponse> Add(Guid purchaseQuoteId, [FromForm] ProductReviewRequest request)
    {
        ProductReviewResponse data = _filterableService.Add(purchaseQuoteId, request);
        return ResponseMessage<ProductReviewResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<ProductReviewResponse> Edit(Guid purchaseQuoteId, Guid id, [FromForm] ProductReviewRequest request)
    {
        ProductReviewResponse data = _filterableService.Edit(purchaseQuoteId, id, request);
        return ResponseMessage<ProductReviewResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<ProductReviewResponse> RemoveById(Guid purchaseQuoteId, Guid id)
    {
        ProductReviewResponse data = _filterableService.RemoveModelById(purchaseQuoteId, id);
        return ResponseMessage<ProductReviewResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<ProductReviewResponse> GetById(Guid purchaseQuoteId, Guid id)
    {
        ProductReviewResponse data = _filterableService.GetModelById(purchaseQuoteId, id);
        return ResponseMessage<ProductReviewResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<ProductReviewPaginatedResponse> FindAll(Guid purchaseQuoteId, [FromQuery] ProductReviewPageableParam param)
    {
        ProductReviewPaginatedResponse data = _service.FindAllModels(purchaseQuoteId, param);
        return ResponseMessage<ProductReviewPaginatedResponse>.Success(data);
    }
}
