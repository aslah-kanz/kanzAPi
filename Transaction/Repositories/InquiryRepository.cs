using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Repositories;

public class InquiryRepository(CommonDbContext context)
: CrudRepository<Inquiry, int?>(context, context.Inquiries), IInquiryRepository
{ }
