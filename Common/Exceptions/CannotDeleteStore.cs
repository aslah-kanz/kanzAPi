using KanzApi.Utils;

namespace KanzApi.Common.Exceptions
{
    public class CannotDeleteStore(string message) : CommonException(ErrorCode.DuplicateStore, message) { }

}