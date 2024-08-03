using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Common.Services;

public class UniqueConstraintViolationException(string value) : CommonException(ErrorCode.UniqueConstraintViolation, value) { }
