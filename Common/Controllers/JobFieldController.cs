using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/job-fields")]
public class JobFieldController(IJobFieldService service) : ControllerBase
{

    private readonly IJobFieldService _service = service;

    [HttpPost]
    public ResponseMessage<JobFieldResponse> Add([FromBody] JobFieldRequest request)
    {
        JobFieldResponse data = _service.Add(request);
        return ResponseMessage<JobFieldResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<JobFieldResponse> Edit(int id, [FromBody] JobFieldRequest request)
    {
        JobFieldResponse data = _service.Edit(id, request);
        return ResponseMessage<JobFieldResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<JobFieldResponse> RemoveById(int id)
    {
        JobFieldResponse data = _service.RemoveModelById(id);
        return ResponseMessage<JobFieldResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<JobFieldResponse> GetById(int id)
    {
        JobFieldResponse data = _service.GetModelById(id);
        return ResponseMessage<JobFieldResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<JobFieldResponse>> FindAll([FromQuery] JobFieldSearchableParam param)
    {
        IEnumerable<JobFieldResponse> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<JobFieldResponse>>.Success(data);
    }
}
