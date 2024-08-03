using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/faq-groups")]
public class FaqGroupController(IFaqGroupService service) : ControllerBase
{

    private readonly IFaqGroupService _service = service;

    [HttpPost]
    public ResponseMessage<FaqGroupResponse> Add([FromBody] FaqGroupRequest request)
    {
        FaqGroupResponse data = _service.Add(request);
        return ResponseMessage<FaqGroupResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<FaqGroupResponse> Edit(int id, [FromBody] FaqGroupRequest request)
    {
        FaqGroupResponse data = _service.Edit(id, request);
        return ResponseMessage<FaqGroupResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<FaqGroupResponse> RemoveById(int id)
    {
        FaqGroupResponse data = _service.RemoveModelById(id);
        return ResponseMessage<FaqGroupResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<FaqGroupResponse> GetById(int id)
    {
        FaqGroupResponse data = _service.GetModelById(id);
        return ResponseMessage<FaqGroupResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<FaqGroupResponse>> FindAll([FromQuery] FaqGroupPageableParam param)
    {
        PaginatedResponse<FaqGroupResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<FaqGroupResponse>>.Success(data);
    }
}
