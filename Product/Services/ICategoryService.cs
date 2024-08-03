using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;

namespace KanzApi.Product.Services;

public interface ICategoryService : ICrudService<Category, int?>
{

    CategoryResponse Add(CategoryRequest request);

    CategoryResponse Edit(int id, CategoryRequest request);

    CategoryResponse RemoveModelById(int id);

    CategoryResponse GetModelById(int id);

    Category GetBySlug(string slug);

    CategoryResponse GetModelBySlug(string slug);

    void CollectSlugs(ISet<string> slugs);

    IEnumerable<CategoryItem> FindAllModels(CategorySearchableParam param);

    IEnumerable<LinkedCategoryItem> FindAllRootModels(CategorySearchableParam param);
}
