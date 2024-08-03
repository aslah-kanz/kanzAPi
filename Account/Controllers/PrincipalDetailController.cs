using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/principal-details")]
public class PrincipalDetailController(IPrincipalDetailFilterableService service,
IPrincipalService principalService) : ControllerBase
{

    private readonly IPrincipalDetailFilterableService _service = service;

    private readonly IPrincipalService _principalService = principalService;

    [HttpPost]
    public ResponseMessage<PrincipalDetailResponse> Add([FromBody] PrincipalDetailRequest request)
    {
        PrincipalDetailResponse data = _service.Add(request);
        return ResponseMessage<PrincipalDetailResponse>.Success(data);
    }

    [Obsolete("Moved to: /account/request-approval")]
    [HttpPost("request-approval")]
    public ResponseMessage<bool> RequestApproval()
    {
        _principalService.RequestApproval();
        return ResponseMessage<bool>.Success(true);
    }

    [HttpPut("{id}")]
    public ResponseMessage<PrincipalDetailResponse> Edit(int id, [FromBody] PrincipalDetailRequest request)
    {
        PrincipalDetailResponse data = _service.Edit(id, request);
        return ResponseMessage<PrincipalDetailResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<PrincipalDetailResponse> GetById(int id)
    {
        PrincipalDetailResponse data = _service.GetModelById(id);
        return ResponseMessage<PrincipalDetailResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<PrincipalDetailResponse>> FindAll([FromQuery] PrincipalDetailPageableParam param)
    {
        PaginatedResponse<PrincipalDetailResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<PrincipalDetailResponse>>.Success(data);
    }
}
