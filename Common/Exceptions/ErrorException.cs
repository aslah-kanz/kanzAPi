using System.Net;
using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class ErrorException(HttpStatusCode code)
: CommonException(ErrorCode.Error, (int)code, code.ToString())
{ }
