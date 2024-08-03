using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class WishListRepository(CommonDbContext context)
: CrudRepository<WishList, int?>(context, context.WishLists), IWishListRepository
{ }
