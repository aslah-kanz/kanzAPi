using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IWebPageService : ICrudService<WebPage, int?>
{
    WebPageResponse Add(WebPageRequest request);

    WebPageResponse Edit(int id, WebPageRequest request);

    WebPageResponse RemoveModelById(int id);

    WebPageResponse GetModelById(int id);

    WebPageResponse GetBySlug(string slug);

    IEnumerable<WebPageResponse> FindAllModels(WebPageSearchableParam param);
}
