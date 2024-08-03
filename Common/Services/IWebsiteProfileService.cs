using KanzApi.Common.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Common.Services;

public interface IWebsiteProfileService : ICrudService<WebsiteProfile, int?>
{
    WebsiteProfileResponse Add(WebsiteProfileRequest request);

    IEnumerable<WebsiteProfileResponse> FindAllModels();
}
