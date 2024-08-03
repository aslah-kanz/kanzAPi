using KanzApi.Common.Repositories;
using KanzApi.Product.Entities;

namespace KanzApi.Product.Repositories;

public interface IProductImageRepository : ICrudRepository<ProductImage, int?> { }
