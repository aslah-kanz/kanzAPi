using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class InvalidStateChangeException(string from, string to) : CommonException(ErrorCode.InvalidStateChange, from, to)
{ }
