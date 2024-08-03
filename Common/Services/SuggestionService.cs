using KanzApi.Common.Models;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class SuggestionService(ISuggestionRepository repository, IMapper mapper)
: CrudService<Suggestion, int?>(repository), ISuggestionService
{

    private readonly IMapper _mapper = mapper;

    public SuggestionResponse Add(SuggestionRequest request)
    {
        Suggestion entity = _mapper.Map<Suggestion>(request);
        entity = Add(entity);

        return _mapper.Map<SuggestionResponse>(entity);
    }

    public SuggestionResponse GetModelById(int id)
    {
        Suggestion entity = GetById(id);
        return _mapper.Map<SuggestionResponse>(entity);
    }
}
