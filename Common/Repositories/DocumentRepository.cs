using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;
using KanzApi.Common.Repositories;

namespace KanzApi.Product.Repositories;

public class DocumentRepository(CommonDbContext context)
: CrudRepository<Document, long?>(context, context.Documents), IDocumentRepository
{ }
