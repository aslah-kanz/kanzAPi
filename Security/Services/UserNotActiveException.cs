using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Security.Services;

public class UserNotActiveException() : CommonException(ErrorCode.UserNotActive) { }
