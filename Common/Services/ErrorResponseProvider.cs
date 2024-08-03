using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using KanzApi.Extensions;
using KanzApi.Resources;
using KanzApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace KanzApi.Common.Services;

public class ErrorResponseProvider(IStringLocalizer<ErrorMessages> localizer,
IWebHostEnvironment webHostEnvironment, ILogger<ErrorResponseProvider> logger)
: IErrorResponseProvider
{

    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

    private readonly ILogger<ErrorResponseProvider> _logger = logger;

    public ResponseMessage<T> Create<T>(string code, string? message, T? data)
    {
        return ResponseMessage<T>.Error(code, message != null ? _localizer.GetString(message) : default(string), data);
    }

    public ResponseMessage<object?> Create(string code, string? message)
    {
        return Create<object?>(code, message, null);
    }

    public ResponseMessage<Dictionary<string, string[]?>> Create(ActionContext context)
    {
        ErrorCode code = ErrorCode.Validation;
        Dictionary<string, string[]?> data = context.ModelState
        .Where(p => p.Value != null && p.Value.Errors.Any())
        .ToDictionary(
            s => s.Key.ToLowerFirstChar(),
            s => s.Value?.Errors.Select(e => e.ErrorMessage).ToArray());

        return Create(code.Key, code.Value, data);
    }

    public ResponseMessage<object?> Create(CommonException e)
    {
        object? data = e.DataObject;

        _logger.LogError(0, e, "Error:");

        if (_webHostEnvironment.IsDevelopment())
        {
            List<ErrorDetail>? errors = [];
            Exception? ex = e;
            while (ex != null)
            {
                errors.Add(ErrorDetail.From(ex));
                ex = ex.InnerException;
            }

            Dictionary<string, object?> dData = data?.ToJsonDictionary() ?? [];
            dData["errors"] = errors;
            data = dData;
        }

        ErrorCode code = e.Code;
        LocalizedString message = e.Args.Length > 0
        ? _localizer.GetString(code.Value, e.Args) : _localizer.GetString(code.Value);

        return Create<object?>(code.Key, message, data);
    }

    private CommonException ToCommonException(Exception e)
    {
        if (e is CommonException ce)
            return ce;
        else if (e is SecurityTokenExpiredException)
            return new TokenExpiredException(e);
        else if (e is SecurityTokenInvalidSignatureException
        || e is SecurityTokenMalformedException)
            return new InvalidTokenException(e);
        else if (e is SqlException se)
            return DatabaseErrorException.From(se, se.Errors);
        else
            return new UnknownException(e);
    }

    public ResponseMessage<object?> Create(Exception e)
    {
        return Create(ToCommonException(e));
    }
}
