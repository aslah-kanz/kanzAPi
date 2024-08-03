using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class WebPageService(IWebPageRepository repository, IMapper mapper)
: CrudService<WebPage, int?>(repository), IWebPageService
{

    private readonly IMapper _mapper = mapper;

    public WebPageResponse Add(WebPageRequest request)
    {
        WebPage entity = _mapper.Map<WebPage>(request);
        entity = Add(entity);

        return _mapper.Map<WebPageResponse>(entity);
    }

    public WebPageResponse Edit(int id, WebPageRequest request)
    {
        WebPage entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<WebPageResponse>(entity);
    }

    public WebPageResponse RemoveModelById(int id)
    {
        WebPage entity = RemoveById(id);
        return _mapper.Map<WebPageResponse>(entity);
    }

    public WebPageResponse GetModelById(int id)
    {
        WebPage entity = GetById(id);
        return _mapper.Map<WebPageResponse>(entity);
    }

    public WebPageResponse GetBySlug(string slug)
    {
        WebPage entity = GetModelBySlug(slug);
        return _mapper.Map<WebPageResponse>(entity);
    }

    public WebPage GetModelBySlug(string slug)
    {
        return FindByPredicate(WebPage.QSlugEquals(slug))
        ?? throw EntityNotFoundException.From(typeof(WebPage), "Slug", slug);
    }

    public IEnumerable<WebPageResponse> FindAllModels(WebPageSearchableParam param)
    {
        IEnumerable<WebPage> pEntity = FindAll(param);

        return pEntity.Select(_mapper.Map<WebPageResponse>);
    }
}
