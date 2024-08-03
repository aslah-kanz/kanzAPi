using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/countries")]
public class CountryController(ICountryService service) : ControllerBase
{

    private readonly ICountryService _service = service;

    [HttpPost]
    public ResponseMessage<CountryResponse> Add([FromBody] CountryRequest request)
    {
        CountryResponse data = _service.Add(request);
        return ResponseMessage<CountryResponse>.Success(data);
    }

    [HttpGet("{id}")]
    public ResponseMessage<CountryResponse> GetById(int id)
    {
        CountryResponse data = _service.GetModelById(id);
        return ResponseMessage<CountryResponse>.Success(data);
    }

    [HttpPut("{id}")]
    public ResponseMessage<CountryResponse> Edit(int id, [FromBody] CountryRequest request)
    {
        CountryResponse data = _service.Edit(id, request);
        return ResponseMessage<CountryResponse>.Success(data);
    }

    [HttpDelete("{id}")]
    public ResponseMessage<CountryResponse> RemoveById(int id)
    {
        CountryResponse data = _service.RemoveModelById(id);
        return ResponseMessage<CountryResponse>.Success(data);
    }

    [AllowAnonymous]
    [HttpGet]
    public ResponseMessage<IEnumerable<CountryItem>> FindAll([FromQuery] CountrySearchableParam param)
    {
        IEnumerable<CountryItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<CountryItem>>.Success(data);
    }
}
