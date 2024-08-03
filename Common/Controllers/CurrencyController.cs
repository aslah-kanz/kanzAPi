using Asp.Versioning;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/currencies")]
public class CurrencyController(ICurrencyService service) : ControllerBase
{

    private readonly ICurrencyService _service = service;

    [HttpGet]
    public ResponseMessage<IEnumerable<CurrencyItem>> FindAll([FromQuery] CurrencySearchableParam param)
    {
        IEnumerable<CurrencyItem> data = _service.FindAllModels(param);
        return ResponseMessage<IEnumerable<CurrencyItem>>.Success(data);
    }
}
