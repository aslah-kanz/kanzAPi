using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Models;
using KanzApi.Product.Models.Param;

namespace KanzApi.Product.Services;

public interface IBrandService : ICrudService<Brand, int?>
{

    BrandResponse Add(BrandRequest request);

    BrandResponse Edit(int id, BrandRequest request);

    BrandResponse RemoveModelById(int id);

    BrandResponse GetModelById(int id);

    BrandResponse GetModelBySlug(string slug);

    PaginatedResponse<BrandItem> FindAllModels(BrandPageableParam param);
}
