using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class InvalidPurchaseQuoteConfirmedQuantityException(Guid id, int quantity, int totalQuantity)
: CommonException(ErrorCode.InvalidPurchaseQuoteConfirmedQuantity, id, quantity, totalQuantity)
{ }
