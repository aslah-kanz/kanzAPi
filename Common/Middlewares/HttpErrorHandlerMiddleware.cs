using KanzApi.Common.Exceptions;
using KanzApi.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace KanzApi.Common.Middlewares;

public class HttpErrorHandlerMiddleware(RequestDelegate next, IErrorResponseProvider errorResponseProvider)
{

    private readonly RequestDelegate _next = next;

    private readonly IErrorResponseProvider _errorResponseProvider = errorResponseProvider;

    private CommonException ToException(HttpContext context)
    {
        HttpStatusCode code = (HttpStatusCode)context.Response.StatusCode;
        return code switch
        {
            HttpStatusCode.Forbidden
            => new ForbiddenException(context.Request.Path),
            HttpStatusCode.MovedPermanently or HttpStatusCode.NotFound
            => new PathNotFoundException(context.Request.Path),
            HttpStatusCode.Unauthorized
            => new UnauthorizedException(context.Request.Path),
            _
            => new ErrorException(code)
        };
    }

    public async Task Invoke(HttpContext context, IOptions<JsonOptions> options)
    {
        HttpResponse response = context.Response;
        Stream originalStream = response.Body;
        using MemoryStream memoryStream = new();
        response.Body = memoryStream;

        try
        {
            await _next(context);


            HttpStatusCode code = (HttpStatusCode)response.StatusCode;
            if (code != HttpStatusCode.OK) throw ToException(context);
        }
        catch (Exception e)
        {
            response.Clear();

            response.ContentType = MediaTypeNames.Application.Json;
            response.StatusCode = (int)HttpStatusCode.OK;

            JsonSerializerOptions jsOptions = options.Value.JsonSerializerOptions;
            string text = JsonSerializer.Serialize(_errorResponseProvider.Create(e), jsOptions);
            await response.WriteAsync(text);
        }

        memoryStream.Position = 0;
        await memoryStream.CopyToAsync(originalStream);
    }
}
