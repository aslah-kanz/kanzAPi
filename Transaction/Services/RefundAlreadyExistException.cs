using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class RefundAlreadyExistException(Guid Id) : CommonException(ErrorCode.RefundAlreadyExist, Id) { }
