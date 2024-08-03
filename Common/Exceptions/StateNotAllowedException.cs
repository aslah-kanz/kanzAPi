using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class StateNotAllowedException(string value) : CommonException(ErrorCode.StateNotAllowed, value)
{ }
