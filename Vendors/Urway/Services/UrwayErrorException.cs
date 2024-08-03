using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Urway.Services;

public class UrwayErrorException(string code, string message)
: CommonException(ErrorCode.PaymentError, code, message)
{ }
