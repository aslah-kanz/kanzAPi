using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class ExchangeTimeExpiredException(Guid itemId) : CommonException(ErrorCode.ExchangeTimeExpired, itemId) { }