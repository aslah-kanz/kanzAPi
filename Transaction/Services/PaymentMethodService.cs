using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;

namespace KanzApi.Transaction.Services;

public class PaymentMethodService(IPaymentMethodRepository repository, IMapper mapper)
: CrudService<PaymentMethod, int?>(repository), IPaymentMethodService
{
    private readonly IMapper _mapper = mapper;

    public PaymentMethodResponse Add(PaymentMethodRequest request)
    {
        PaymentMethod entity = _mapper.Map<PaymentMethod>(request);
        entity = Add(entity);

        return _mapper.Map<PaymentMethodResponse>(entity);
    }

    public PaymentMethodResponse Edit(int id, PaymentMethodRequest request)
    {
        PaymentMethod entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<PaymentMethodResponse>(entity);
    }

    public PaymentMethodResponse RemoveModelById(int id)
    {
        PaymentMethod entity = RemoveById(id);
        return _mapper.Map<PaymentMethodResponse>(entity);
    }

    public PaymentMethodResponse GetModelById(int id)
    {
        PaymentMethod entity = GetById(id);
        return _mapper.Map<PaymentMethodResponse>(entity);
    }

    public IEnumerable<PaymentMethodItem> FindAllModels(PaymentMethodSearchableParam param)
    {
        IEnumerable<PaymentMethod> pEntity = FindAll(param);

        return pEntity.Select(_mapper.Map<PaymentMethodItem>);
    }
}
