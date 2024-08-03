using KanzApi.Account.Entities;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;

namespace KanzApi.Account.Repositories;

public class DepartmentRepository(CommonDbContext context)
: CrudRepository<Department, int?>(context, context.Departments), IDepartmentRepository
{ }
