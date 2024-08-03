using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class ProductMismatchException(int id, int expectedId) : CommonException(ErrorCode.ProductMismatch, id, expectedId) { }
