using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class PrivilegeRepository(CommonDbContext context)
: CrudRepository<Privilege, int?>(context, context.Privileges), IPrivilegeRepository
{ }
