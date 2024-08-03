using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class CancelOrderNotAllowedException(Guid Id) : CommonException(ErrorCode.CancelOrderNotAllowed, Id) { }
