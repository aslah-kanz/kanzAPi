using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class CatalogueController(ICatalogueService service) : ControllerBase
{

    private readonly ICatalogueService _service = service;

    [HttpPost]
    public ResponseMessage<CatalogueResponse> Add([FromBody] CatalogueRequest request)
    {
        CatalogueResponse data = _service.Add(request);
        return ResponseMessage<CatalogueResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public ResponseMessage<CatalogueResponse> Edit(int id, [FromBody] CatalogueRequest request)
    {
        CatalogueResponse data = _service.Edit(id, request);
        return ResponseMessage<CatalogueResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<CatalogueResponse> RemoveById(int id)
    {
        CatalogueResponse data = _service.RemoveModelById(id);
        return ResponseMessage<CatalogueResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<CatalogueResponse> GetById(int id)
    {
        CatalogueResponse data = _service.GetModelById(id);
        return ResponseMessage<CatalogueResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<CatalogueResponse>> FindAll([FromQuery] CataloguePageableParam param)
    {
        PaginatedResponse<CatalogueResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<CatalogueResponse>>.Success(data);
    }
}
