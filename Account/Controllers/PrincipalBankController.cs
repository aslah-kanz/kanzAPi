using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/principal-banks")]
public class PrincipalBankController(IPrincipalBankFilterableService service) : ControllerBase
{

    private readonly IPrincipalBankFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<PrincipalBankResponse> Add([FromBody] PrincipalBankRequest request)
    {
        PrincipalBankResponse data = _service.Add(request);
        return ResponseMessage<PrincipalBankResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<PrincipalBankResponse> Edit(int id, [FromBody] PrincipalBankRequest request)
    {
        PrincipalBankResponse data = _service.Edit(id, request);
        return ResponseMessage<PrincipalBankResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<PrincipalBankResponse> RemoveById(int id)
    {
        PrincipalBankResponse data = _service.RemoveModelById(id);
        return ResponseMessage<PrincipalBankResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<PrincipalBankResponse> GetById(int id)
    {
        PrincipalBankResponse data = _service.GetModelById(id);
        return ResponseMessage<PrincipalBankResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<PrincipalBankResponse>> FindAll([FromQuery] PrincipalBankPageableParam param)
    {
        PaginatedResponse<PrincipalBankResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<PrincipalBankResponse>>.Success(data);
    }
}
