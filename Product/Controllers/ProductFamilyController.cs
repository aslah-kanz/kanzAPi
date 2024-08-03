using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[AllowAnonymous]
[Tags("SaleProduct")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/product-families")]
public class ProductFamilyController(ISaleProductFilterableService service) : ControllerBase
{

    private readonly ISaleProductFilterableService _service = service;

    [HttpGet("{code}/products")]
    public ResponseMessage<PaginatedResponse<ProductFamilyProductItem>> FindAllProductsByCode(
        [FromQuery] SaleProductPageableParam param, string code)
    {
        PaginatedResponse<ProductFamilyProductItem> data = _service.FindAllModelsByFamilyCode(param, code);
        return ResponseMessage<PaginatedResponse<ProductFamilyProductItem>>.Success(data);
    }

    [HttpGet("{code}/products-pdp")]
    public ResponseMessage<PaginatedResponse<ProductFamilyProductItem>> FindAllProductsByCodePDP(
       [FromQuery] SaleProductPageableParam param, string code)
    {
        PaginatedResponse<ProductFamilyProductItem> data = _service.FindAllModelsByFamilyCodePDP(param, code);
        return ResponseMessage<PaginatedResponse<ProductFamilyProductItem>>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<ProductFamilyPaginatedResponse> FindAll([FromQuery] ProductFamilyPageableParam param)
    {
        ProductFamilyPaginatedResponse data = _service.FindAllFamilies(param);
        return ResponseMessage<ProductFamilyPaginatedResponse>.Success(data);
    }
}
