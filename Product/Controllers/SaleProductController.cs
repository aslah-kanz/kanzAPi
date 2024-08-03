using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Product.Controllers;

[AllowAnonymous]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/sale-products")]
public class SaleProductController(ISaleProductService service, ISaleProductFilterableService filterableService) : ControllerBase
{

    private readonly ISaleProductService _service = service;

    private readonly ISaleProductFilterableService _filterableService = filterableService;

    [HttpGet("{id}")]
    public ResponseMessage<SaleProductResponse> GetById(int id)
    {
        SaleProductResponse data = _service.GetModelById(id);
        return ResponseMessage<SaleProductResponse>.Success(data);
    }

    [HttpGet("{id}/price-list")]
    public ResponseMessage<SaleProductPriceListResponse> GetProductPriceList(int id)
    {
        SaleProductPriceListResponse data = _service.GetProductPriceListById(id);
        return ResponseMessage<SaleProductPriceListResponse>.Success(data);
    }

    [HttpGet("{id}/price-list-by-country/{countryCode}")]
    public ResponseMessage<SaleProductPriceListResponse> GetProductPriceListByCountryCode(int id, string countryCode)
    {
        SaleProductPriceListResponse data = _service.GetProductPriceListByIdAndCountryCode(id, countryCode);
        return ResponseMessage<SaleProductPriceListResponse>.Success(data);
    }

    [HttpGet("slug/{slug}")]
    public ResponseMessage<SaleProductResponse> GetBySlug(string slug)
    {
        SaleProductResponse data = _service.GetModelBySlug(slug);
        return ResponseMessage<SaleProductResponse>.Success(data);
    }

    [HttpGet("{id}/related-product-families")]
    public ResponseMessage<IEnumerable<string>> FindAllRelatedFamiliesById(int id)
    {
        IEnumerable<string> data = _filterableService.FindAllRelatedFamiliesById(id);
        return ResponseMessage<IEnumerable<string>>.Success(data);
    }

    [HttpGet("slug/{slug}/related-product-families")]
    public ResponseMessage<IEnumerable<string>> FindAllRelatedFamiliesBySlug(string slug)
    {
        IEnumerable<string> data = _filterableService.FindAllRelatedFamiliesBySlug(slug);
        return ResponseMessage<IEnumerable<string>>.Success(data);
    }
}
