using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class TokenExpiredException : CommonException
{

    public TokenExpiredException() : base(ErrorCode.TokenExpired) { }

    public TokenExpiredException(Exception? innerException) : base(innerException, ErrorCode.TokenExpired) { }
}
