using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface ILanguageService : ICrudService<Language, int?>
{
    LanguageResponse Add(LanguageRequest request);

    LanguageResponse Edit(int id, LanguageRequest request);

    LanguageResponse RemoveModelById(int id);

    LanguageResponse GetModelById(int id);

    IEnumerable<LanguageItem> FindAllModels(LanguageSearchableParam param);
}
