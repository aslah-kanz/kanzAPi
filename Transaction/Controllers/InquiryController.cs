using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Transaction.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/inquiries")]
public class InquiryController(IInquiryFilterableService service) : ControllerBase
{

    private readonly IInquiryFilterableService _service = service;

    [HttpPost]
    public ResponseMessage<InquiryResponse> Add([FromBody] AddInquiryRequest request)
    {
        InquiryResponse data = _service.Add(request);
        return ResponseMessage<InquiryResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<InquiryResponse> Edit(int id, [FromBody] EditInquiryRequest request)
    {
        InquiryResponse data = _service.Edit(id, request);
        return ResponseMessage<InquiryResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<InquiryResponse> RemoveById(int id)
    {
        InquiryResponse data = _service.RemoveModelById(id);
        return ResponseMessage<InquiryResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<InquiryResponse> GetById(int id)
    {
        InquiryResponse data = _service.GetModelById(id);
        return ResponseMessage<InquiryResponse>.Success(data);
    }

    [HttpGet]
    public ResponseMessage<PaginatedResponse<InquiryResponse>> FindAll([FromQuery] InquiryPageableParam param)
    {
        PaginatedResponse<InquiryResponse> data = _service.FindAllModels(param);
        return ResponseMessage<PaginatedResponse<InquiryResponse>>.Success(data);
    }

    [HttpGet("product-ids")]
    public ResponseMessage<IEnumerable<int>> FindAll()
    {
        IEnumerable<int> data = _service.FindAllProductIds();
        return ResponseMessage<IEnumerable<int>>.Success(data);
    }
}
