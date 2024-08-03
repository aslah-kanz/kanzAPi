using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;

namespace KanzApi.Transaction.Services;

public class ExchangeService(IExchangeRepository repository, IMapper mapper)
: CrudService<Exchange, Guid?>(repository), IExchangeService
{

    private readonly IMapper _mapper = mapper;

    public ExchangeResponse AdminChangeStatus(Guid id,
    EExchangeStatus fromStatus, EExchangeStatus toStatus, string comment)
    {
        Exchange entity = GetById(id);
        if (entity.Status != fromStatus)
        {
            throw new InvalidStateChangeException(entity.Status.ToString()!, toStatus.ToString());
        }

        entity.Status = toStatus;
        entity.AdminComment = comment;
        entity = Edit(entity);

        return _mapper.Map<ExchangeResponse>(entity);
    }

    public ExchangeResponse AdminApprove(Guid id, string comment)
    {
        return AdminChangeStatus(id, EExchangeStatus.Pending, EExchangeStatus.Reviewed, comment!);
    }

    public ExchangeResponse AdminReject(Guid id, string comment)
    {
        return AdminChangeStatus(id, EExchangeStatus.Pending, EExchangeStatus.Rejected, comment!);
    }

    public ExchangeResponse VendorChangeStatus(Guid id,
    EExchangeStatus fromStatus, EExchangeStatus toStatus, string comment)
    {
        Exchange entity = GetById(id);
        if (entity.Status != fromStatus)
        {
            throw new InvalidStateChangeException(entity.Status.ToString()!, toStatus.ToString());
        }

        entity.Status = toStatus;
        entity.VendorComment = comment;
        entity = Edit(entity);

        return _mapper.Map<ExchangeResponse>(entity);
    }

    public ExchangeResponse VendorApprove(Guid id, string comment)
    {
        return VendorChangeStatus(id, EExchangeStatus.Reviewed, EExchangeStatus.Approved, comment!);
    }

    public ExchangeResponse VendorReject(Guid id, string comment)
    {
        return VendorChangeStatus(id, EExchangeStatus.Reviewed, EExchangeStatus.Rejected, comment!);
    }

    public AdminExchangeResponse GetAdminModelById(Guid id)
    {
        Exchange entity = GetById(id);
        return _mapper.Map<AdminExchangeResponse>(entity);
    }

    public PaginatedResponse<AdminExchangeResponse> FindAllAdminModels(AdminExchangePageableParam param)
    {
        PaginatedEntity<Exchange> pEntity = FindAll(param);
        IEnumerable<AdminExchangeResponse> models = pEntity.Content.Select(_mapper.Map<AdminExchangeResponse>);

        return PaginatedResponse<AdminExchangeResponse>.From(pEntity, models);
    }
}
