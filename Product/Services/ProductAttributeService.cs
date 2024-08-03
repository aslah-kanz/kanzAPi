using KanzApi.Common.Services;
using KanzApi.Product.Entities;
using KanzApi.Product.Repositories;

namespace KanzApi.Product.Services;

public class ProductAttributeService(IProductAttributeRepository repository)
: CrudService<ProductAttribute, Guid?>(repository), IProductAttributeService
{

    public IEnumerable<ProductAttribute> FindAllByProductId(int productId)
    {
        return FindAll(ProductAttribute.QProductIdEquals(productId), null);
    }
}
