using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.Graph.Services;

public class GraphUnknownException() : CommonException(ErrorCode.MailUnknown) { }
