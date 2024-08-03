using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using AttributeEntity = KanzApi.Product.Entities.Attribute;

namespace KanzApi.Product.Repositories;

public class AttributeRepository(CommonDbContext context)
: CrudRepository<AttributeEntity, int?>(context, context.Attributes), IAttributeRepository
{ }
