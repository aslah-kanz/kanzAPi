using KanzApi.Account.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;

namespace KanzApi.Transaction.Services;

public class WithdrawalService(IWithdrawalRepository repository, IMapper mapper)
: CrudService<Withdraw, int?>(repository), IWithdrawalService
{

    private readonly IMapper _mapper = mapper;

    public override Withdraw Add(Withdraw entity)
    {
        throw new NotSupportedException();
    }

    public override Withdraw Edit(Withdraw entity)
    {
        throw new NotSupportedException();
    }

    public override Withdraw Remove(Withdraw entity)
    {
        throw new NotSupportedException();
    }

    public WithdrawalResponse UpdateStatus(int Id, EWithdrawStatus status)
    {
        Withdraw entity = GetById(Id);
        entity.Status = status;

        entity = Edit(entity);

        return _mapper.Map<WithdrawalResponse>(entity);
    }

    public PaginatedResponse<WithdrawalResponse> FindAllModels(WithdrawalPageableParam param)
    {
        var activePage = (param.Page < 1) ? 1 : param.Page;
        Page page = new(activePage, param.Size);
        PaginatedEntity<Withdraw> pEntity = FindAll(page, param.ToPredicate(), Sort.From(param));
        IEnumerable<WithdrawalResponse> models = pEntity.Content.Select(_mapper.Map<WithdrawalResponse>);

        return PaginatedResponse<WithdrawalResponse>.From(pEntity, models);
    }
}
