using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Oto.Services;

public class OtoErrorException(string code, string message)
: CommonException(ErrorCode.DeliveryError, code, message)
{ }
