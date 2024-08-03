using System.Net;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KanzApi.Common.Filters;

public class HttpResponseExceptionFilter(IErrorResponseProvider errorResponseProvider)
: IActionFilter
{

    private readonly IErrorResponseProvider _errorResponseProvider = errorResponseProvider;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new ObjectResult(_errorResponseProvider.Create(context))
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Exception? e = context.Exception;
        if (e != null)
        {
            ResponseMessage<object?> value = _errorResponseProvider.Create(e);
            context.Result = new ObjectResult(value)
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            context.ExceptionHandled = true;
        }
    }
}
