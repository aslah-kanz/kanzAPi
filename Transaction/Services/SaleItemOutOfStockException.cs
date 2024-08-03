using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Transaction.Services;

public class SaleItemOutOfStockException(int id, int stock) : CommonException(ErrorCode.SaleItemOutOfStock, id, stock) { }
