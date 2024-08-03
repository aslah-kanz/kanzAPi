using Asp.Versioning;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]s")]
public class DocumentController(IDocumentService service) : ControllerBase
{

    private readonly IDocumentService _service = service;

    [HttpPost]
    public ResponseMessage<DocumentResponse> Add([FromForm] DocumentRequest request)
    {
        DocumentResponse data = _service.Add(request);
        return ResponseMessage<DocumentResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<DocumentResponse> Edit(long id, [FromForm] DocumentRequest request)
    {
        DocumentResponse data = _service.Edit(id, request);
        return ResponseMessage<DocumentResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<DocumentResponse> RemoveById(long id)
    {
        DocumentResponse data = _service.RemoveModelById(id);
        return ResponseMessage<DocumentResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<DocumentResponse> GetById(long id)
    {
        DocumentResponse data = _service.GetModelById(id);
        return ResponseMessage<DocumentResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet("download/{name}")]
    public IActionResult DownloadByName(string name)
    {
        Document entity = _service.GetByName(name);
        FileStream fs = new(_service.FilePath(entity), FileMode.Open);

        return File(fs, entity.Type!);
    }
}
