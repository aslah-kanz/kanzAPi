using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class FaqRepository(CommonDbContext context)
: CrudRepository<Faq, int?>(context, context.Faqs), IFaqRepository
{ }
