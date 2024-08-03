using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public interface IExchangeRepository : ICrudRepository<Exchange, Guid?> { }
