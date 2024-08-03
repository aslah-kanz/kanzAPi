using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class InvalidTokenException(Exception? innerException) : CommonException(innerException, ErrorCode.InvalidToken)
{

    public InvalidTokenException() : this(null) { }
}
