using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class InvalidRefundQuantityException(Guid itemId, int quantity) : CommonException(ErrorCode.InvalidRefundQuantity, itemId, quantity) { }
