using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/customer-profiles")]
public class CustomerProfileController(ICustomerProfileService service) : ControllerBase
{

    private readonly ICustomerProfileService _service = service;

    [HttpPut]
    public ResponseMessage<CustomerProfileResponse> Edit([FromForm] CustomerProfileRequest request)
    {
        CustomerProfileResponse data = _service.Edit(request);
        return ResponseMessage<CustomerProfileResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<CustomerProfileResponse> GetById()
    {
        CustomerProfileResponse data = _service.GetModelById();
        return ResponseMessage<CustomerProfileResponse>.Success(data);
    }
}
