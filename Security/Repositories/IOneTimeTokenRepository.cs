using KanzApi.Common.Repositories;
using KanzApi.Security.Entities;

namespace KanzApi.Security.Repositories;

public interface IOneTimeTokenRepository : ICrudRepository<OneTimeToken, int?> { }
