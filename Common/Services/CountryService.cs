using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using MapsterMapper;

namespace KanzApi.Common.Services;

public class CountryService(ICountryRepository repository, IMapper mapper)
: CrudService<Country, int?>(repository), ICountryService
{
    private readonly IMapper _mapper = mapper;

    public CountryResponse Add(CountryRequest request)
    {
        Country entity = _mapper.Map<Country>(request);
        entity = Add(entity);

        return _mapper.Map<CountryResponse>(entity);
    }

    public CountryResponse Edit(int id, CountryRequest request)
    {
        Country entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<CountryResponse>(entity);
    }

    public CountryResponse RemoveModelById(int id)
    {
        Country entity = RemoveById(id);
        return _mapper.Map<CountryResponse>(entity);
    }

    public CountryResponse GetModelById(int id)
    {
        Country entity = GetById(id);
        return _mapper.Map<CountryResponse>(entity);
    }

    public IEnumerable<CountryItem> FindAllModels(CountrySearchableParam param)
    {
        IEnumerable<Country> pEntity = FindAll(param);

        return pEntity.Select(_mapper.Map<CountryItem>);
    }
}
