using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public interface IWishListRepository : ICrudRepository<WishList, int?> { }
