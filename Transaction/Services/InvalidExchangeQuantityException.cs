using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class InvalidExchangeQuantityException(Guid itemId, int quantity) : CommonException(ErrorCode.InvalidExchangeQuantity, itemId, quantity) { }
