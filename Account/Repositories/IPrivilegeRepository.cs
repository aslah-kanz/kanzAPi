using KanzApi.Account.Entities;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public interface IPrivilegeRepository : ICrudRepository<Privilege, int?> { }
