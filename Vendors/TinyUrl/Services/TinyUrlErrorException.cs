using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.TinyUrl.Services;

public class TinyUrlErrorException(int code, string message)
: CommonException(ErrorCode.UrlShortenerError, code, message)
{ }
