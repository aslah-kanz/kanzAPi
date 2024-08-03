using KanzApi.Common.Services;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Services;

public interface IProductAttributeService : ICrudService<ProductAttribute, Guid?>
{

    IEnumerable<ProductAttribute> FindAllByProductId(int productId);
}
