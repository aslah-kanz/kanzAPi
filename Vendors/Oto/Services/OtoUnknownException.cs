using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Oto.Services;

public class OtoUnknownException() : CommonException(ErrorCode.DeliveryUnknown) { }
