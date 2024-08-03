using System.Linq.Expressions;
using KanzApi.Configurations.Contexts;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using KanzApi.Extensions;
using Microsoft.EntityFrameworkCore;
using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Product.Repositories;

public class ProductRepository(CommonDbContext context)
: CrudRepository<ProductEntity, int?>(context, context.Products), IProductRepository
{

    public ProductEntity? FindByPredicateWithDetails(Expression<Func<ProductEntity, bool>> predicate)
    {
        return _set.Where(predicate)
        .Include("Icon")
        .Include("Image")
        .Include("Brand.Image")
        .Include("Images.Image")
        .Include("Attributes.Property")
        .Include("Attributes.Property")
        .Include("Attributes.Attribute")
        .Include("Attributes.Image")
        .SingleOrDefault();
    }

	public PaginatedEntity<string?> FindAllFamilyCodes(Page page,
    Expression<Func<ProductEntity, bool>>? predicate, Sort? sort)
    {
        IQueryable<ProductEntity> query = _set.Include(e => e.Categories);
        if (predicate != null) query = query.Where(predicate);
        if (sort != null) query = query.Sort(sort);

        IQueryable<string?> sQuery = query
        .GroupBy(e => e.FamilyCode).Select(p => p.Key);

        int count = sQuery.Count();

        IEnumerable<string?> result = [.. sQuery
        .Skip(page.Index * page.Size)
        .Take(page.Size)];

        return new PaginatedEntity<string?>(result, page.Index, page.Size, count);
    }
}
