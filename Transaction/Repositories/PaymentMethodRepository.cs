using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class PaymentMethodRepository(CommonDbContext context)
: CrudRepository<PaymentMethod, int?>(context, context.PaymentMethods), IPaymentMethodRepository
{ }
