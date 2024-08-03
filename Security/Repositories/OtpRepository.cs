using KanzApi.Security.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Security.Repositories;

public class OtpRepository(CommonDbContext context)
: CrudRepository<Otp, int?>(context, context.Otps), IOtpRepository
{ }
