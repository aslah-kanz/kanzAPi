using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class PrincipalNotAllowedException() : CommonException(ErrorCode.PrincipalNotAllowed) { }
