using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Common.Services;

public class UsernameAlreadyExistException(string value) : CommonException(ErrorCode.UsernameAlreadyExist, value) { }
