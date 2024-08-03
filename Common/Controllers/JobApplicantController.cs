using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[Tags("Job")]
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class JobApplicantController(IJobApplicantService service) : ControllerBase
{

    private readonly IJobApplicantService _service = service;

    [HttpPost("{id}/approve")]
    public ResponseMessage<JobApplicantResponse> Approve(int id)
    {
        JobApplicantResponse data = _service.Approve(id);
        return ResponseMessage<JobApplicantResponse>.Success(data);
    }

    [HttpPost("{id}/reject")]
    public ResponseMessage<JobApplicantResponse> Reject(int id)
    {
        JobApplicantResponse data = _service.Reject(id);
        return ResponseMessage<JobApplicantResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<JobApplicantResponse> GetById(int id)
    {
        JobApplicantResponse data = _service.GetModelById(id);
        return ResponseMessage<JobApplicantResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<JobApplicantResponse>> FindAll([FromQuery] JobApplicantPageableParam param)
    {
        PaginatedResponse<JobApplicantResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<JobApplicantResponse>>.Success(data);
    }
}
