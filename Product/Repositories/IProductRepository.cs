using System.Linq.Expressions;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Repositories;

public interface IProductRepository : ICrudRepository<ProductEntity, int?>
{

    ProductEntity? FindByPredicateWithDetails(Expression<Func<ProductEntity, bool>> predicate);

	PaginatedEntity<string?> FindAllFamilyCodes(Page page, Expression<Func<ProductEntity, bool>> predicate, Sort? sort);
}
