using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class SaleItemPriceMismatchException(decimal value, decimal expectedValue)
: CommonException(ErrorCode.SaleItemPriceMismatch, value, expectedValue)
{ }
