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
public class RoleController(IRoleService service) : ControllerBase
{

    private readonly IRoleService _service = service;

    [HttpPost]
    public ResponseMessage<RoleResponse> Add([FromBody] RoleRequest request)
    {
        RoleResponse data = _service.Add(request);
        return ResponseMessage<RoleResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<RoleResponse> Edit(int id, [FromBody] RoleRequest request)
    {
        RoleResponse data = _service.Edit(id, request);
        return ResponseMessage<RoleResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<RoleResponse> RemoveById(int id)
    {
        RoleResponse data = _service.RemoveModelById(id);
        return ResponseMessage<RoleResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<RoleResponse> GetById(int id)
    {
        RoleResponse data = _service.GetModelById(id);
        return ResponseMessage<RoleResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<IEnumerable<RoleItem>> FindAll([FromQuery] RoleSearchableParam param)
    {
        IEnumerable<RoleItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<RoleItem>>.Success(data);
    }
}
