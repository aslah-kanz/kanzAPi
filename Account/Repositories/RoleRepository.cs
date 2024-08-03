using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class RoleRepository(CommonDbContext context)
: CrudRepository<Role, int?>(context, context.Roles), IRoleRepository
{ }
