using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Configurations.Contexts;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class CustomerOrderFilterableService(ICustomerOrderRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
IPrincipalDetailFilterableService principalDetailService, IStoreFilterableService storeService,
IPurchaseQuoteService purchaseQuoteService, CommonDbContext context)
: FilterableCrudService<CustomerOrder, Guid?>(repository), ICustomerOrderFilterableService
{

    private readonly CommonDbContext _context = context;
    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    private readonly IStoreFilterableService _storeService = storeService;

    private readonly IPurchaseQuoteService _purchaseQuoteService = purchaseQuoteService;

    protected override Expression<Func<CustomerOrder, bool>> Filter(Expression<Func<CustomerOrder, bool>>? predicate)
    {
        Principal principal = _principalService.GetCurrent();

        Expression<Func<CustomerOrder, bool>> statusPredicate = CustomerOrder
        .QStatusNotContains([ECustomerOrderStatus.Open, ECustomerOrderStatus.Failed]);
        if (principal.Type == EPrincipalType.Vendor || principal.Type == EPrincipalType.Manufacture)
        {
            ISet<int> storeIds = _storeService.FindAll().Select(e => (int)e.Id!).ToHashSet();
            Expression<Func<CustomerOrder, bool>> filterPredicate = CustomerOrder
            .QStoreOrderStoreIdsEquals(storeIds).And(statusPredicate);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Company)
        {
            PrincipalDetail principalDetail = _principalDetailService.GetByCurrentPrincipal();
            Expression<Func<CustomerOrder, bool>> filterPredicate = CustomerOrder
            .QPrincipalDetailIdEquals((int)principalDetail.Id!).And(statusPredicate);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Individual)
        {
            Expression<Func<CustomerOrder, bool>> filterPredicate = CustomerOrder
            .QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!).And(statusPredicate);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else
        {
            return predicate ?? Expressions.True<CustomerOrder>();
        }
    }

    public PaginatedResponse<Models.CustomerOrderItem> FindAllModels(CustomerOrderPageableParam param)
    {
        PaginatedEntity<CustomerOrder> pEntity = FindAll(param);
        IEnumerable<Models.CustomerOrderItem> models = pEntity.Content.Select(_mapper.Map<Models.CustomerOrderItem>);

        var updatable = models.ToList();
        foreach (var item in updatable)
        {
            var quotes = _purchaseQuoteService.FindAllIds(e => e.CustomerOrderId == item.Id);
            item.IsReviewed = _context.ProductReviews.Any(x => quotes.Contains(x.PurchaseQuoteId));
            if (item.Status == ECustomerOrderStatus.InPayment)
            {
                var expireDate = item.CreatedAt.AddHours(24);
                var timeLeft = expireDate - DateTime.Now;
                item.ExpireTimeLeft = timeLeft.TotalMilliseconds;

                if (item.ExpireTimeLeft < 0)
                {
                    CustomerOrder? order = FindById(item.Id);
                    if (order != null)
                    {
                        order.Status = ECustomerOrderStatus.CanceledBySystem;
                        item.Status = ECustomerOrderStatus.CanceledBySystem;
                        Edit(order);
                    }
                }
            }
        }

        models = updatable.AsEnumerable();
        return PaginatedResponse<Models.CustomerOrderItem>.From(pEntity, models);
    }
}