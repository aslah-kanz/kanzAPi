using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class RefundTimeExpiredException(Guid itemId) : CommonException(ErrorCode.RefundTimeExpired, itemId) { }