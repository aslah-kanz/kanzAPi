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
public class LanguageController(ILanguageService service) : ControllerBase
{

    private readonly ILanguageService _service = service;

    [HttpPost]
    public ResponseMessage<LanguageResponse> Add([FromBody] LanguageRequest request)
    {
        LanguageResponse data = _service.Add(request);
        return ResponseMessage<LanguageResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<LanguageResponse> GetById(int id)
    {
        LanguageResponse data = _service.GetModelById(id);
        return ResponseMessage<LanguageResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<LanguageResponse> Edit(int id, [FromBody] LanguageRequest request)
    {
        LanguageResponse data = _service.Edit(id, request);
        return ResponseMessage<LanguageResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<LanguageResponse> RemoveById(int id)
    {
        LanguageResponse data = _service.RemoveModelById(id);
        return ResponseMessage<LanguageResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<LanguageItem>> FindAll([FromQuery] LanguageSearchableParam param)
    {
        IEnumerable<LanguageItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<LanguageItem>>.Success(data);
    }
}
