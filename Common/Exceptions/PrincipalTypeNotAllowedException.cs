using KanzApi.Account.Entities;
using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class PrincipalTypeNotAllowedException(EPrincipalType type)
: CommonException(ErrorCode.PrincipalTypeNotAllowed, type)
{ }
