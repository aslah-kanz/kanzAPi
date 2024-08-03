using KanzApi.Common.Services;
using AttributeEntity = KanzApi.Product.Entities.Attribute;

namespace KanzApi.Product.Services;

public interface IAttributeService : ICrudService<AttributeEntity, int?> { }
