using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/principal-addresses")]
public class PrincipalAddressController(IPrincipalAddressFilterableService service) : ControllerBase
{

    private readonly IPrincipalAddressFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<PrincipalAddressResponse> Add([FromBody] PrincipalAddressRequest request)
    {
        PrincipalAddressResponse data = _service.Add(request);
        return ResponseMessage<PrincipalAddressResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<PrincipalAddressResponse> Edit(int id, [FromBody] PrincipalAddressRequest request)
    {
        PrincipalAddressResponse data = _service.Edit(id, request);
        return ResponseMessage<PrincipalAddressResponse>.Success(data);
    }

    [HttpPost("{id}/default")]
    public ResponseMessage<PrincipalAddressResponse> ChangeDefault(int id)
    {
        PrincipalAddressResponse data = _service.ChangeDefault(id);
        return ResponseMessage<PrincipalAddressResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<PrincipalAddressResponse> RemoveById(int id)
    {
        PrincipalAddressResponse data = _service.RemoveModelById(id);
        return ResponseMessage<PrincipalAddressResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<PrincipalAddressResponse> GetById(int id)
    {
        PrincipalAddressResponse data = _service.GetModelById(id);
        return ResponseMessage<PrincipalAddressResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<PrincipalAddressItem>> FindAll([FromQuery] PrincipalAddressPageableParam param)
    {
        PaginatedResponse<PrincipalAddressItem> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<PrincipalAddressItem>>.Success(data);
    }
}
