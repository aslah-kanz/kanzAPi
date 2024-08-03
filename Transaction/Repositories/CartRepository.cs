using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class CartRepository(CommonDbContext context)
: CrudRepository<Cart, int?>(context, context.Carts), ICartRepository
{ }
