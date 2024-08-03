using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Common.Middlewares;

public class ForbiddenException(string path) : CommonException(ErrorCode.Forbidden, path) { }
