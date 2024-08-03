using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class LanguageRepository(CommonDbContext context)
: CrudRepository<Language, int?>(context, context.Languages), ILanguageRepository
{ }
