using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/contact-us")]
public class SuggestionController(ISuggestionService service) : ControllerBase
{

    private readonly ISuggestionService _service = service;

    [AllowAnonymous]
    [HttpPost]
    public ResponseMessage<SuggestionResponse> Add([FromBody] SuggestionRequest request)
    {
        SuggestionResponse data = _service.Add(request);
        return ResponseMessage<SuggestionResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<SuggestionResponse> GetById(int id)
    {
        SuggestionResponse data = _service.GetModelById(id);
        return ResponseMessage<SuggestionResponse>.Success(data);
    }
}
