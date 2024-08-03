using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class WithdrawalFilterableService(IWithdrawalRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService, CommonDbContext context)
: FilterableCrudService<Withdraw, int?>(repository), IWithdrawalFilterableService
{

    private readonly ISessionService _sessionService = sessionService;
    private readonly CommonDbContext _dbContext = context;
    private readonly IMapper _mapper = mapper;
    private readonly IPrincipalService _principalService = principalService;

    public WithdrawalResponse Add(WithdrawalRequest request)
    {
        Withdraw entity = _mapper.Map<Withdraw>(request);
        entity.Principal = _principalService.GetById(_sessionService.CurrentPrincipalId() ?? 0);
        entity.Status = EWithdrawStatus.Pending;
        entity.WithdrawMethod = "";
        entity = Add(entity);

        return _mapper.Map<WithdrawalResponse>(entity);
    }

    public WithdrawalResponse Edit(int Id, WithdrawalRequest request)
    {
        Withdraw entity = GetById(Id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<WithdrawalResponse>(entity);
    }

    public PaginatedResponse<WithdrawalResponse> FindAllModels(WithdrawalPageableParam param)
    {
        PaginatedEntity<Withdraw> pEntity = FindAll(param);
        IEnumerable<WithdrawalResponse> models = pEntity.Content.Select(_mapper.Map<WithdrawalResponse>);

        return PaginatedResponse<WithdrawalResponse>.From(pEntity, models);
    }

    public decimal GetAmount()
    {
        var principal = _principalService.GetCurrent();
        var storeIds = _dbContext.Stores.Where(e => e.PrincipalId == principal.Id).Select(e => e.Id);
        var result = _dbContext.PurchaseQuotes.Where(e => storeIds.Contains(e.StoreId) && e.Status == EPurchaseQuoteStatus.Delivered).Sum(e => e.SubTotal);
        var totalWithdrawal = _dbContext.Withdraws.Where(w => w.PrincipalId == principal.Id && w.Status == EWithdrawStatus.Completed).Sum(e => e.Amount);
        result -= totalWithdrawal;
        return result.Value;
    }

    protected override Expression<Func<Withdraw, bool>> Filter(Expression<Func<Withdraw, bool>>? predicate)
    {
        Expression<Func<Withdraw, bool>> filterPredicate = Withdraw.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }
}
