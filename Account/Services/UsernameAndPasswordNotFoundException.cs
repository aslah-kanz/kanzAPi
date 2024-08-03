using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Account.Services;

public class UsernameAndPasswordNotFoundException() : CommonException(ErrorCode.UsernameAndPasswordNotFound) { }
