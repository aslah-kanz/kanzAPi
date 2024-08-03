using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class EmptyCartException() : CommonException(ErrorCode.EmptyCart) { }
