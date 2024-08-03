using KanzApi.Common.Services;
using KanzApi.Product.Repositories;
using AttributeEntity = KanzApi.Product.Entities.Attribute;

namespace KanzApi.Product.Services;

public class AttributeService(IAttributeRepository repository)
: CrudService<AttributeEntity, int?>(repository), IAttributeService
{ }
