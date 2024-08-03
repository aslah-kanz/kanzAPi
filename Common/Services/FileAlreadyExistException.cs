using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Common.Services;

public class FileAlreadyExistException(string name) : CommonException(ErrorCode.FileAlreadyExist, name) { }
