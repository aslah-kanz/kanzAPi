using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Product.Services;

public class RecursiveParentException(int id) : CommonException(ErrorCode.RecursiveParent, id) { }
