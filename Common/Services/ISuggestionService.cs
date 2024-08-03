using KanzApi.Common.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Common.Services;

public interface ISuggestionService : ICrudService<Suggestion, int?>
{
    SuggestionResponse Add(SuggestionRequest request);

    SuggestionResponse GetModelById(int id);
}
