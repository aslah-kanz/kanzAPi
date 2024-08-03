using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using MapsterMapper;
using KanzApi.Common.Repositories;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Services;

public class BlogService(IBlogRepository repository, IMapper mapper)
: CrudService<Blog, int?>(repository), IBlogService
{

    private readonly IMapper _mapper = mapper;

    public BlogResponse Add(BlogRequest request)
    {
        Blog entity = _mapper.Map<Blog>(request);
        entity = Add(entity);

        return _mapper.Map<BlogResponse>(entity);
    }

    public BlogResponse Edit(int id, BlogRequest request)
    {
        Blog entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<BlogResponse>(entity);
    }

    public BlogResponse RemoveModelById(int id)
    {
        Blog entity = RemoveById(id);
        return _mapper.Map<BlogResponse>(entity);
    }

    public BlogResponse GetModelById(int id)
    {
        Blog entity = GetById(id);
        return _mapper.Map<BlogResponse>(entity);
    }

    public PaginatedResponse<BlogResponse> FindAllModels(BlogPageableParam param)
    {
        PaginatedEntity<Blog> pEntity = FindAll(param);
        IEnumerable<BlogResponse> models = pEntity.Content.Select(_mapper.Map<BlogResponse>);

        return PaginatedResponse<BlogResponse>.From(pEntity, models);
    }
}
