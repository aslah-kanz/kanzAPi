using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Vendors.SendGrid.Services;

public class SendGridErrorException(int code, string message)
: CommonException(ErrorCode.MailError, code, message)
{ }
