using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface ICountryService : ICrudService<Country, int?>
{
    CountryResponse Add(CountryRequest request);

    CountryResponse Edit(int id, CountryRequest request);

    CountryResponse RemoveModelById(int id);

    CountryResponse GetModelById(int id);

    IEnumerable<CountryItem> FindAllModels(CountrySearchableParam param);
}
