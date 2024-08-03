using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Msegat.Services;

public class MsegatUnknownException() : CommonException(ErrorCode.OtpUnknown) { }
