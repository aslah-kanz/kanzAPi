using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/company-members")]
public class CompanyMemberController(IPrincipalDetailService service,
IPrincipalDetailFilterableService filterableService) : ControllerBase
{

    private readonly IPrincipalDetailService _service = service;

    private readonly IPrincipalDetailFilterableService _filterableService = filterableService;

    [HttpPost]
    public ResponseMessage<CompanyMemberResponse> Add([FromBody] CompanyMemberRequest request)
    {
        CompanyMemberResponse data = _filterableService.AddMember(request);
        return ResponseMessage<CompanyMemberResponse>.Success(data);
    }

    [HttpPut("{principalId}")]
    public ResponseMessage<CompanyMemberResponse> Edit(int principalId, [FromBody] CompanyMemberRequest request)
    {
        CompanyMemberResponse data = _service.EditMember(principalId, request);
        return ResponseMessage<CompanyMemberResponse>.Success(data);
    }

    [HttpDelete("{principalId}")]
    public ResponseMessage<CompanyMemberResponse> RemoveById(int principalId)
    {
        CompanyMemberResponse data = _service.DisableMember(principalId);
        return ResponseMessage<CompanyMemberResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<CustomerItem>> FindAll([FromQuery] CompanyMemberPageableParam param)
    {
        PaginatedResponse<CustomerItem> data = _filterableService.FindAllCompanyMembers(param);
        return ResponseMessage<PaginatedResponse<CustomerItem>>.Success(data);
    }
}
