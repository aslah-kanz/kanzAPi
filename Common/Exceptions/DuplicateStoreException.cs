using KanzApi.Utils;

namespace KanzApi.Common.Exceptions
{
    public class DuplicateStoreError(string message) : CommonException(ErrorCode.DuplicateStore, message) { }

}