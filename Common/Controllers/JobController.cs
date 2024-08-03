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
public class JobController(IJobService service, IJobApplicantService jobApplicantService) : ControllerBase
{

    private readonly IJobService _service = service;

    private readonly IJobApplicantService _jobApplicantService = jobApplicantService;

    [HttpPost]
    public ResponseMessage<JobResponse> Add([FromBody] JobRequest request)
    {
        JobResponse data = _service.Add(request);
        return ResponseMessage<JobResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<JobResponse> Edit(int id, [FromBody] JobRequest request)
    {
        JobResponse data = _service.Edit(id, request);
        return ResponseMessage<JobResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<JobResponse> RemoveById(int id)
    {
        JobResponse data = _service.RemoveModelById(id);
        return ResponseMessage<JobResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<JobResponse> GetById(int id)
    {
        JobResponse data = _service.GetModelById(id);
        return ResponseMessage<JobResponse>.Success(data);
    }

    [HttpGet("{id}/applicants")]
    public ResponseMessage<IEnumerable<JobApplicantResponse>> FindAllByJobId(int id)
    {
        IEnumerable<JobApplicantResponse> data = _jobApplicantService.FindAllByJobId(id);
        return ResponseMessage<IEnumerable<JobApplicantResponse>>.Success(data);
    }

    [AllowAnonymous]
    [HttpPost("{id}/apply")]
    public ResponseMessage<JobApplicantResponse> Apply(int id, [FromForm] JobApplicantRequest request)
    {
        JobApplicantResponse data = _jobApplicantService.Add(id, request);
        return ResponseMessage<JobApplicantResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<JobResponse>> FindAll([FromQuery] JobPageableParam param)
    {
        PaginatedResponse<JobResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<JobResponse>>.Success(data);
    }
}
