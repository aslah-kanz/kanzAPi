using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class ExchangeAlreadyExistException(Guid Id) : CommonException(ErrorCode.ExchangeAlreadyExist, Id) { }
