using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Urway.Services;

public class UrwayUnknownException() : CommonException(ErrorCode.PaymentUnknown) { }
