using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class PrincipalController(IPrincipalService service) : ControllerBase
{

    private readonly IPrincipalService _service = service;

    [Authorize(Policy = Privileges.AddPrincipal)]
    [HttpPost]
    public ResponseMessage<PrincipalResponse> Add([FromBody] PrincipalRequest request)
    {
        PrincipalResponse data = _service.Add(request);
        return ResponseMessage<PrincipalResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.EditPrincipal)]
    [HttpPut("{id}")]
    public ResponseMessage<PrincipalResponse> Edit(int id, [FromBody] EditPrincipalRequest request)
    {
        PrincipalResponse data = _service.Edit(id, request);
        return ResponseMessage<PrincipalResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.RemovePrincipal)]
    [HttpDelete("{id}")]
    public ResponseMessage<PrincipalResponse> RemoveById(int id)
    {
        PrincipalResponse data = _service.RemoveModelById(id);
        return ResponseMessage<PrincipalResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewPrincipal)]
    [HttpGet("{id}")]
    public ResponseMessage<PrincipalResponse> GetById(int id)
    {
        PrincipalResponse data = _service.GetModelById(id);
        return ResponseMessage<PrincipalResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewPrincipal)]
    [HttpPost("{id}/approve")]
    public ResponseMessage<PrincipalResponse> Approve(int id)
    {
        PrincipalResponse data = _service.Approve(id);
        return ResponseMessage<PrincipalResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewPrincipal)]
    [HttpPost("{id}/reject")]
    public ResponseMessage<PrincipalResponse> Reject(int id)
    {
        PrincipalResponse data = _service.Reject(id);
        return ResponseMessage<PrincipalResponse>.Success(data);
    }

    [Authorize(Policy = Privileges.ViewPrincipal)]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<PrincipalResponse>> FindAll([FromQuery] PrincipalPageableParam param)
    {
        PaginatedResponse<PrincipalResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<PrincipalResponse>>.Success(data);
    }
}
