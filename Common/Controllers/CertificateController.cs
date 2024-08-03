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
public class CertificateController(ICertificateService service) : ControllerBase
{

    private readonly ICertificateService _service = service;

    [HttpPost]
    public ResponseMessage<CertificateResponse> Add([FromForm] CertificateRequest request)
    {
        CertificateResponse data = _service.Add(request);
        return ResponseMessage<CertificateResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<CertificateResponse> Edit(int id, [FromForm] CertificateRequest request)
    {
        CertificateResponse data = _service.Edit(id, request);
        return ResponseMessage<CertificateResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<CertificateResponse> RemoveById(int id)
    {
        CertificateResponse data = _service.RemoveModelById(id);
        return ResponseMessage<CertificateResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<CertificateResponse> GetById(int id)
    {
        CertificateResponse data = _service.GetModelById(id);
        return ResponseMessage<CertificateResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<PaginatedResponse<CertificateResponse>> FindAll([FromQuery] CertificatePageableParam param)
    {
        PaginatedResponse<CertificateResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<CertificateResponse>>.Success(data);
    }
}
