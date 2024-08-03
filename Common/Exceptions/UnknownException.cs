using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class UnknownException(Exception? innerException)
: CommonException(innerException, ErrorCode.Unknown)
{ }
