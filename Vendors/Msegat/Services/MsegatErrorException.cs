using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Msegat.Services;

public class MsegatErrorException(string code, string message)
: CommonException(ErrorCode.OtpError, code, message)
{ }
