using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class WithdrawalRepository(CommonDbContext context)
: CrudRepository<Withdraw, int?>(context, context.Withdraws), IWithdrawalRepository
{ }
