using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Common.Middlewares;

public class PathNotFoundException(string path) : CommonException(ErrorCode.PathNotFound, path) { }
