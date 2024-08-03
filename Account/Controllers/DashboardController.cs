using Asp.Versioning;
using KanzApi.Account.Models;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Account.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class DashboardController(IDashboardService service) : ControllerBase
{
    private readonly IDashboardService _service = service;

    [HttpGet("vendor")]
    public ResponseMessage<VendorDashboardResponse> GetVendorDashboard()
    {
        VendorDashboardResponse data = _service.GetVendorDashboard();
        return ResponseMessage<VendorDashboardResponse>.Success(data);
    }

}
