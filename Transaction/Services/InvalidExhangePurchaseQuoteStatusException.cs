using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class InvalidExchangePurchaseQuoteStatusException(Guid itemId) : CommonException(ErrorCode.InvalidExchangePurchaseQuoteStatus, itemId) { }