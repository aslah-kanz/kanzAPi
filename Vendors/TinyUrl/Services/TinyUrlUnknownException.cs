using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.TinyUrl.Services;

public class TinyUrlUnknownException() : CommonException(ErrorCode.UrlShortenerUnknown) { }
