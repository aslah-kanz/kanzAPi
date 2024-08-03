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
public class PrivilegeController(IPrivilegeService service) : ControllerBase
{

    private readonly IPrivilegeService _service = service;

    [HttpPost]
    public ResponseMessage<PrivilegeResponse> Add([FromBody] PrivilegeRequest request)
    {
        PrivilegeResponse data = _service.Add(request);
        return ResponseMessage<PrivilegeResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<PrivilegeResponse> Edit(int id, [FromBody] PrivilegeRequest request)
    {
        PrivilegeResponse data = _service.Edit(id, request);
        return ResponseMessage<PrivilegeResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<PrivilegeResponse> RemoveById(int id)
    {
        PrivilegeResponse data = _service.RemoveModelById(id);
        return ResponseMessage<PrivilegeResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<PrivilegeResponse> GetById(int id)
    {
        PrivilegeResponse data = _service.GetModelById(id);
        return ResponseMessage<PrivilegeResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<IEnumerable<PrivilegeResponse>> FindAll([FromQuery] PrivilegeSearchableParam param)
    {
        IEnumerable<PrivilegeResponse> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<PrivilegeResponse>>.Success(data);
    }
}
