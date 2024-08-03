using KanzApi.Common.Repositories;
using KanzApi.Security.Entities;

namespace KanzApi.Security.Repositories;

public interface IOtpRepository : ICrudRepository<Otp, int?> { }
