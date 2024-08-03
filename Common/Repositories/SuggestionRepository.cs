using KanzApi.Configurations.Contexts;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Repositories;

public class SuggestionRepository(CommonDbContext context)
: CrudRepository<Suggestion, int?>(context, context.Suggestions), ISuggestionRepository
{ }
