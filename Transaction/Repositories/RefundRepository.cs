using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class RefundRepository(CommonDbContext context)
: CrudRepository<Refund, Guid?>(context, context.Refunds), IRefundRepository
{ }
