using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class UnauthorizedException(string path) : CommonException(ErrorCode.Unauthorized, path) { }
