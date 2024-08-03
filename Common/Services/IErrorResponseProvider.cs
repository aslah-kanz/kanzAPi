using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace KanzApi.Common.Services;

public interface IErrorResponseProvider

{

    ResponseMessage<T> Create<T>(string code, string? message, T? data);

    ResponseMessage<object?> Create(string code, string? message);

    ResponseMessage<Dictionary<string, string[]?>> Create(ActionContext context);

    ResponseMessage<object?> Create(CommonException e);

    ResponseMessage<object?> Create(Exception e);
}
