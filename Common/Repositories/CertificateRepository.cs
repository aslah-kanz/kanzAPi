using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class CertificateRepository(CommonDbContext context)
: CrudRepository<Certificate, int?>(context, context.Certificates), ICertificateRepository
{ }
