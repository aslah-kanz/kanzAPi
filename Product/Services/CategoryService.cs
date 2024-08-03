using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;
using KanzApi.Product.Models.Builders;
using KanzApi.Product.Repositories;
using MapsterMapper;

namespace KanzApi.Product.Services;

public class CategoryService(ICategoryRepository repository, IMapper mapper)
: CrudService<Category, int?>(repository), ICategoryService
{

    private readonly IMapper _mapper = mapper;

    public override Category Add(Category entity)
    {
        entity.Path = entity.StringifiedPath();
        return base.Add(entity);
    }

    public override Category Edit(Category entity)
    {
        entity.Path = entity.StringifiedPath();
        return base.Edit(entity);
    }

    public CategoryResponse Add(CategoryRequest request)
    {
        Category entity = _mapper.Map<Category>(request);
        entity = Add(entity);

        return _mapper.Map<CategoryResponse>(entity);
    }

    public CategoryResponse Edit(int id, CategoryRequest request)
    {
        Category entity = GetById(id);
        _mapper.Map(request, entity);

        if (entity.ParentId == id) throw new RecursiveParentException(id);

        entity = Edit(entity);

        return _mapper.Map<CategoryResponse>(entity);
    }

    public CategoryResponse RemoveModelById(int id)
    {
        Category entity = RemoveById(id);
        return _mapper.Map<CategoryResponse>(entity);
    }

    public CategoryResponse GetModelById(int id)
    {
        Category entity = GetById(id);
        return _mapper.Map<CategoryResponse>(entity);
    }

    public Category GetBySlug(string slug)
    {
        return FindByPredicate(Category.QSlugEquals(slug))
        ?? throw EntityNotFoundException.From(typeof(Category), "Slug", slug);
    }

    public CategoryResponse GetModelBySlug(string slug)
    {
        Category entity = GetBySlug(slug);
        return _mapper.Map<CategoryResponse>(entity);
    }

    private void CollectSlugs(ISet<string> slugs, Category entity)
    {
        if (slugs.Contains(entity.Slug!))
        {
            return;
        }

        slugs.Add(entity.Slug!);

        foreach (Category c in entity.Children)
        {
            CollectSlugs(slugs, c);
        }
    }

    public void CollectSlugs(ISet<string> slugs)
    {
        HashSet<string> tempSlugs = [];
        foreach (string slug in slugs)
        {
            if (tempSlugs.Contains(slug))
            {
                continue;
            }

            Category entity = GetBySlug(slug);
            CollectSlugs(tempSlugs, entity);
        }

        slugs.UnionWith(tempSlugs);
    }

    public IEnumerable<CategoryItem> FindAllModels(CategorySearchableParam param)
    {
        IEnumerable<Category> entities = FindAll(param);
        return entities.Select(_mapper.Map<CategoryItem>);
    }

    public IEnumerable<LinkedCategoryItem> FindAllRootModels(CategorySearchableParam param)
    {
        IEnumerable<Category> entities = FindAll(param);
        LinkedCategoryItemBuilder builder = new(_mapper, entities);

        if (param.Brands.Any())
        {
            builder.SetService(this);
        }

        return builder.Build();
    }
}
