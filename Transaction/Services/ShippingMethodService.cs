using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;

namespace KanzApi.Transaction.Services;

public class ShippingMethodService(IShippingMethodRepository repository, IMapper mapper)
: CrudService<ShippingMethod, int?>(repository), IShippingMethodService
{

    private readonly IMapper _mapper = mapper;

    public ShippingMethodResponse Add(ShippingMethodRequest request)
    {
        ShippingMethod entity = _mapper.Map<ShippingMethod>(request);
        entity = Add(entity);

        return _mapper.Map<ShippingMethodResponse>(entity);
    }

    public ShippingMethodResponse Edit(int id, ShippingMethodRequest request)
    {
        ShippingMethod entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<ShippingMethodResponse>(entity);
    }

    public ShippingMethodResponse RemoveModelById(int id)
    {
        ShippingMethod entity = RemoveById(id);
        return _mapper.Map<ShippingMethodResponse>(entity);
    }

    public ShippingMethodResponse GetModelById(int id)
    {
        ShippingMethod entity = GetById(id);
        return _mapper.Map<ShippingMethodResponse>(entity);
    }

    public IEnumerable<ShippingMethodItem> FindAllModels(ShippingMethodSearchableParam param)
    {
        IEnumerable<ShippingMethod> pEntity = FindAll(param);

        return pEntity.Select(_mapper.Map<ShippingMethodItem>);
    }
}
