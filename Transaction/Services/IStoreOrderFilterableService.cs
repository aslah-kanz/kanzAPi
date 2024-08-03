using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Services;

public interface IStoreOrderFilterableService : IFilterableCrudService<StoreOrder, Guid?> { }
