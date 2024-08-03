using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class StoreController(IStoreFilterableService service, IStoreActionService storeActionService) : ControllerBase
{

    private readonly IStoreFilterableService _service = service;

    private readonly IStoreActionService _storeActionService = storeActionService;

    [HttpPost]
    public ResponseMessage<StoreResponse> Add([FromBody] StoreRequest request)
    {
        StoreResponse data = _service.Add(request);
        return ResponseMessage<StoreResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<StoreResponse> Edit(int id, [FromBody] StoreRequest request)
    {
        StoreResponse data = _service.Edit(id, request);
        return ResponseMessage<StoreResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<StoreResponse> RemoveById(int id)
    {
        StoreResponse data = _service.RemoveModelById(id);
        return ResponseMessage<StoreResponse>.Success(data);
    }

    [HttpPost("{id}/activate")]
    public ResponseMessage<StoreResponse> ActivateById(int id)
    {
        StoreResponse data = _storeActionService.Activate(id);
        return ResponseMessage<StoreResponse>.Success(data);
    }

    [HttpPost("{id}/inactivate")]
    public ResponseMessage<StoreResponse> InactivateById(int id)
    {
        StoreResponse data = _storeActionService.Inactivate(id);
        return ResponseMessage<StoreResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<StoreResponse> GetById(int id)
    {
        StoreResponse data = _service.GetModelById(id);
        return ResponseMessage<StoreResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<StoreResponse>> FindAll([FromQuery] StorePageableParam param)
    {
        PaginatedResponse<StoreResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<StoreResponse>>.Success(data);
    }

    [HttpGet("name")]
    public ResponseMessage<IEnumerable<NameableResponse>> FindAllNameables([FromQuery] StoreSearchableParam param)
    {
        IEnumerable<NameableResponse> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<NameableResponse>>.Success(data);
    }
}
