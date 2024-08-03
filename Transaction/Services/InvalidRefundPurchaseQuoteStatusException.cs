using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class InvalidRefundPurchaseQuoteStatusException(Guid itemId) : CommonException(ErrorCode.InvalidRefundPurchaseQuoteStatus, itemId) { }