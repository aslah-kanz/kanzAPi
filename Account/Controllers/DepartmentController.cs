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
public class DepartmentController(IDepartmentFilterableService service) : ControllerBase
{

    private readonly IDepartmentFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<DepartmentResponse> Add([FromBody] DepartmentRequest request)
    {
        DepartmentResponse data = _service.Add(request);
        return ResponseMessage<DepartmentResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<DepartmentResponse> Edit(int id, [FromBody] DepartmentRequest request)
    {
        DepartmentResponse data = _service.Edit(id, request);
        return ResponseMessage<DepartmentResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<DepartmentResponse> RemoveById(int id)
    {
        DepartmentResponse data = _service.RemoveModelById(id);
        return ResponseMessage<DepartmentResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<DepartmentResponse> GetById(int id)
    {
        DepartmentResponse data = _service.GetModelById(id);
        return ResponseMessage<DepartmentResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<DepartmentResponse>> FindAll([FromQuery] DepartmentPageableParam param)
    {
        PaginatedResponse<DepartmentResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<DepartmentResponse>>.Success(data);
    }
}
