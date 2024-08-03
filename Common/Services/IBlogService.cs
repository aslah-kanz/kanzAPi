using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;

namespace KanzApi.Common.Services;

public interface IBlogService : ICrudService<Blog, int?>
{
    BlogResponse Add(BlogRequest request);

    BlogResponse Edit(int id, BlogRequest request);

    BlogResponse RemoveModelById(int id);

    BlogResponse GetModelById(int id);

    PaginatedResponse<BlogResponse> FindAllModels(BlogPageableParam param);
}
