using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Repositories;
using MapsterMapper;

namespace KanzApi.Product.Services;

public class BrandService(IBrandRepository repository, IMapper mapper, ICategoryService categoryService)
: CrudService<Brand, int?>(repository), IBrandService
{

    private readonly IMapper _mapper = mapper;
    private readonly ICategoryService _categoryService = categoryService;

    public BrandResponse Add(BrandRequest request)
    {
        Brand entity = _mapper.Map<Brand>(request);
        entity = Add(entity);

        return _mapper.Map<BrandResponse>(entity);
    }

    public BrandResponse Edit(int id, BrandRequest request)
    {
        Brand entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<BrandResponse>(entity);
    }

    public BrandResponse RemoveModelById(int id)
    {
        Brand entity = RemoveById(id);
        return _mapper.Map<BrandResponse>(entity);
    }

    public BrandResponse GetModelById(int id)
    {
        Brand entity = GetById(id);
        return _mapper.Map<BrandResponse>(entity);
    }

    public BrandResponse GetModelBySlug(string slug)
    {
        Brand entity = FindByPredicate(Brand.QSlugEquals(slug))
        ?? throw EntityNotFoundException.From(typeof(Brand), "Slug", slug);
        return _mapper.Map<BrandResponse>(entity);
    }

    public PaginatedResponse<BrandItem> FindAllModels(BrandPageableParam param)
    {
        _categoryService.CollectSlugs(param.Categories);

        PaginatedEntity<Brand> pEntity = FindAll(param);
        IEnumerable<BrandItem> models = pEntity.Content.Select(_mapper.Map<BrandItem>);

        return PaginatedResponse<BrandItem>.From(pEntity, models);
    }
}
