using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;
using MapsterMapper;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public class CurrencyService(ICurrencyRepository repository, IMapper mapper)
: CrudService<Currency, int?>(repository), ICurrencyService
{

    private readonly IMapper _mapper = mapper;

    public IEnumerable<CurrencyItem> FindAllModels(CurrencySearchableParam param)
    {
        IEnumerable<Currency> pEntity = FindAll(param);
        return pEntity.Select(_mapper.Map<CurrencyItem>);
    }
}
