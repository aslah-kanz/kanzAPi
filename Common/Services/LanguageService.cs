using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using MapsterMapper;

namespace KanzApi.Common.Services;

public class LanguageService(ILanguageRepository repository, IMapper mapper)
: CrudService<Language, int?>(repository), ILanguageService
{
    private readonly IMapper _mapper = mapper;

    public LanguageResponse Add(LanguageRequest request)
    {
        Language entity = _mapper.Map<Language>(request);
        entity = Add(entity);

        return _mapper.Map<LanguageResponse>(entity);
    }

    public LanguageResponse Edit(int id, LanguageRequest request)
    {
        Language entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<LanguageResponse>(entity);
    }

    public LanguageResponse RemoveModelById(int id)
    {
        Language entity = RemoveById(id);
        return _mapper.Map<LanguageResponse>(entity);
    }

    public LanguageResponse GetModelById(int id)
    {
        Language entity = GetById(id);
        return _mapper.Map<LanguageResponse>(entity);
    }

    public IEnumerable<LanguageItem> FindAllModels(LanguageSearchableParam param)
    {
        IEnumerable<Language> pEntity = FindAll(param);

        return pEntity.Select(_mapper.Map<LanguageItem>);
    }
}
