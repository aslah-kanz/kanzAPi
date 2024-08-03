using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface ICurrencyService : ICrudService<Currency, int?>
{

    IEnumerable<CurrencyItem> FindAllModels(CurrencySearchableParam param);
}
