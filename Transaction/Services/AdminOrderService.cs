using KanzApi.Common.Models;
using KanzApi.Common.Services;
using MapsterMapper;
using KanzApi.Transaction.Repositories;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public class AdminOrderService(ICustomerOrderRepository repository, IMapper mapper)
: CrudService<CustomerOrder, Guid?>(repository), IAdminOrderService
{

    private readonly IMapper _mapper = mapper;

    public AdminOrderResponse Cancel(Guid id)
    {
        CustomerOrder entity = GetById(id);
        if (entity.Status == ECustomerOrderStatus.Open ||
            entity.Status == ECustomerOrderStatus.WaitingApproval ||
            entity.Status == ECustomerOrderStatus.WaitingPayment)
        {
            entity.Status = ECustomerOrderStatus.Canceled;
            entity = Edit(entity);
        } 
        else
        {
            throw new CancelOrderNotAllowedException(id);
        }

        return _mapper.Map<AdminOrderResponse>(entity);
    }

    public AdminOrderResponse Complete(Guid id)
    {
        CustomerOrder entity = GetById(id);
        entity.Status = ECustomerOrderStatus.Completed;
        entity = Edit(entity);

        return _mapper.Map<AdminOrderResponse>(entity);
    }

    public AdminOrderResponse RemoveModelById(Guid id)
    {
        CustomerOrder entity = RemoveById(id);
        return _mapper.Map<AdminOrderResponse>(entity);
    }

    public AdminOrderResponse GetModelById(Guid id)
    {
        CustomerOrder entity = GetById(id);
        return _mapper.Map<AdminOrderResponse>(entity);
    }

    public PaginatedResponse<AdminOrderItem> FindAllModels(AdminOrderPageableParam param)
    {
        PaginatedEntity<CustomerOrder> pEntity = FindAll(param);
        IEnumerable<AdminOrderItem> models = pEntity.Content.Select(_mapper.Map<AdminOrderItem>);

        return PaginatedResponse<AdminOrderItem>.From(pEntity, models);
    }
}
