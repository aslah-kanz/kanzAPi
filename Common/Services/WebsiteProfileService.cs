using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Repositories;
using MapsterMapper;

namespace KanzApi.Common.Services;

public class WebsiteProfileService(IWebsiteProfileRepository repository, IMapper mapper, IImageService imageService)
: CrudService<WebsiteProfile, int?>(repository), IWebsiteProfileService
{
    private readonly IMapper _mapper = mapper;

    private readonly IImageService _imageService = imageService;

    public WebsiteProfileResponse Add(WebsiteProfileRequest request)
    {
        WebsiteProfile entity = _mapper.Map<WebsiteProfile>(request);

        if (request.Logo != null)
        {
            Image image = _imageService.AddWithRandomName(request.Logo, EImageGroup.WebsiteProfile);
            entity.Image = image;
        }

        if (request.Favicon != null)
        {
            Image image = _imageService.AddWithRandomName(request.Favicon, EImageGroup.WebsiteProfile);
            entity.Favicon = image;
        }

        entity = Add(entity);

        return _mapper.Map<WebsiteProfileResponse>(entity);
    }

    public IEnumerable<WebsiteProfileResponse> FindAllModels()
    {
        IEnumerable<WebsiteProfile> pEntity = FindAll();

        return pEntity.Select(_mapper.Map<WebsiteProfileResponse>);
    }
}
