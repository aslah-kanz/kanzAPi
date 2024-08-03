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
public class FaqController(IFaqService service) : ControllerBase
{

    private readonly IFaqService _service = service;

    [HttpPost]
    public ResponseMessage<FaqResponse> Add([FromBody] FaqRequest request)
    {
        FaqResponse data = _service.Add(request);
        return ResponseMessage<FaqResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<FaqResponse> Edit(int id, [FromBody] FaqRequest request)
    {
        FaqResponse data = _service.Edit(id, request);
        return ResponseMessage<FaqResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<FaqResponse> RemoveById(int id)
    {
        FaqResponse data = _service.RemoveModelById(id);
        return ResponseMessage<FaqResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public ResponseMessage<FaqResponse> GetById(int id)
    {
        FaqResponse data = _service.GetModelById(id);
        return ResponseMessage<FaqResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<FaqResponse>> FindAll([FromQuery] FaqPageableParam param)
    {
        PaginatedResponse<FaqResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<FaqResponse>>.Success(data);
    }
}
