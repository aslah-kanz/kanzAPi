using KanzApi.Common.Repositories;
using AttributeEntity = KanzApi.Product.Entities.Attribute;

namespace KanzApi.Product.Repositories;

public interface IAttributeRepository : ICrudRepository<AttributeEntity, int?> { }
