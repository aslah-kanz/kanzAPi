using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Product.Models;
using KanzApi.Product.Services;
using Meilisearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Product.Controllers;

[AllowAnonymous] // TODO: Remove after development
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/search-engine")]
public class SearchEngineController(ISearchEngineService<ProductFamilyProductItemMeilisearch> service) : ControllerBase
{

    private readonly ISearchEngineService<ProductFamilyProductItemMeilisearch> _service = service;

	[HttpPost("sync-products")]
	public async Task<ResponseMessage<TaskInfoStatus>> SyncProductItem()
	{
		var result = await _service.SyncProducts();
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}

	[HttpPost("sync-products/{familyCode}")]
	public ResponseMessage<TaskInfoStatus> SyncProductItemByFamilyCode(string familyCode)
	{
		var result = _service.SyncProductsByFamilyCode(familyCode);
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}
	
	[HttpPost("sync-products/product")]
	public async Task<ResponseMessage<TaskInfoStatus>> SyncProductItemById([FromBody] List<int> ids)
	{
		var result = await _service.SyncProductsById(ids);
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}

	//[HttpPost("add-products")]
	//public async Task<ResponseMessage<TaskInfoStatus>> Add([FromBody] IEnumerable<ProductFamilyProductItemMeilisearch> documents)
	//{
	//	var result = await _service.AddOrReplace(documents);
	//	return ResponseMessage<TaskInfoStatus>.Success(result);
	//}
	
	//[HttpDelete("delete-products")]
	//public async Task<ResponseMessage<TaskInfoStatus>> Delete([FromBody] IEnumerable<int> ids)
	//{
	//	var result = await _service.Delete(ids);
	//	return ResponseMessage<TaskInfoStatus>.Success(result);
	//}
	
	[HttpPut("update-filterable")]
	public async Task<ResponseMessage<TaskInfoStatus>> UpdateFilterable([SwaggerParameter("<i>Current Indices</i> : products, families")] string indexName)
	{
		var result = await _service.FilterableSettings(indexName);
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}
	
	[HttpPut("update-searchable")]
	public async Task<ResponseMessage<TaskInfoStatus>> UpdateSearchable([SwaggerParameter("<i>Current Indices</i> : products, families")] string indexName)
	{
		var result = await _service.SearchableSettings(indexName);
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}
	
	[HttpPut("update-sortable")]
	public async Task<ResponseMessage<TaskInfoStatus>> UpdateSortable([SwaggerParameter("<i>Current Indices</i> : products, families")] string indexName)
	{
		var result = await _service.SortableSettings(indexName);
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}
	
	[HttpPut("update-faceted")]
	public async Task<ResponseMessage<TaskInfoStatus>> UpdateFaceted([SwaggerParameter("<i>Current Indices</i> : products, families")] string indexName)
	{
		var result = await _service.FacetedSettings(indexName);
		return ResponseMessage<TaskInfoStatus>.Success(result);
	}

}
