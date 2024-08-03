using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Entities;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using KanzApi.Utils;
using MapsterMapper;
using System.Linq.Expressions;
using System.Transactions;

namespace KanzApi.Transaction.Services;

public class ExchangeFilterableService(IExchangeRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
IPrincipalDetailFilterableService principalDetailService, IImageService imageService, IStoreFilterableService storeService)
: FilterableCrudService<Exchange, Guid?>(repository), IExchangeFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IImageService _imageService = imageService;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    private readonly IStoreFilterableService _storeService = storeService;

    private string GenerateNumber(int principalId)
    {
        string prefix = "EX";
        string currentDateTime = DateTime.Now.ToString("yyMMdd-HHmmss");
        return $"{prefix}-{currentDateTime}-{principalId}";
    }

    public ExchangeResponse Add(ExchangeRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Exchange? entity = FindByPurchaseQuoteId((Guid)request.PurchaseQuote!);
        if (entity == null)
        {
            entity = _mapper.Map<Exchange>(request);

            Principal principal = _principalService.GetCurrent();
            entity.Principal = principal;

            PurchaseQuote purchaseQuote = entity.PurchaseQuote!;
            if (entity.PurchaseQuote!.CustomerOrder!.PrincipalId != principal.Id)
            {
                throw new PrincipalNotAllowedException();
            }

            if (purchaseQuote.Status != EPurchaseQuoteStatus.Delivered)
            {
                throw new InvalidExchangePurchaseQuoteStatusException((Guid)entity.PurchaseQuoteId!);
            }

            if (!DateUtils.SetIsExchangeableIsRefundable(purchaseQuote.UpdatedAt))
            {
                throw new ExchangeTimeExpiredException((Guid)entity.PurchaseQuoteId!);
            }

            entity.PrincipalDetail = _principalDetailService.FindByCurrentPrincipal();
            entity.Number = GenerateNumber((int)entity.PrincipalId!);

            foreach (IFormFile file in request.Files!)
            {
                Image image = _imageService.AddWithRandomName(file, EImageGroup.Exchange);
                entity.Images.Add(image);
            }

            int maxQuantity = entity.PurchaseQuote?.ConfirmedQuantity ?? 0;
            if (request.Quantity <= maxQuantity)
            {
                entity.SubTotal = request.Quantity * entity.Price;
            }
            else
            {
                throw new InvalidExchangeQuantityException((Guid)entity.PurchaseQuoteId!, maxQuantity);
            }

            entity = Add(entity);
        }
        else
        {
            throw new ExchangeAlreadyExistException((Guid)entity.Id!);
        }

        ExchangeResponse response = _mapper.Map<ExchangeResponse>(entity);

        scope.Complete();

        return response;
    }

    public ExchangeResponse RemoveModelById(Guid id)
    {
        Exchange entity = GetById(id);
        entity.Status = EExchangeStatus.Canceled;

        return _mapper.Map<ExchangeResponse>(entity);
    }

    public ExchangeResponse GetModelById(Guid id)
    {
        Exchange entity = GetById(id);
        return _mapper.Map<ExchangeResponse>(entity);
    }

    public Exchange? FindByPurchaseQuoteId(Guid purchaseQuoteId)
    {
        return _repository.FindByPredicate(Filter(
            Exchange.QPurchaseQuoteIdEquals(purchaseQuoteId).And(Exchange.QStatusNotEquals(EExchangeStatus.Rejected))));
    }

    protected override Expression<Func<Exchange, bool>> Filter(Expression<Func<Exchange, bool>>? predicate)
    {
        Principal principal = _principalService.GetCurrent();
        if (principal.Type == EPrincipalType.Vendor)
        {
            List<int> storeIds = _storeService.FindAll().Select(e => (int)e.Id!).ToList();
            Expression<Func<Exchange, bool>> filterPredicate = Exchange
            .QPurchaseQuoteStoreIdsEquals(storeIds).And(Exchange.QStatusNotEquals(EExchangeStatus.Pending));
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Company)
        {
            PrincipalDetail principalDetail = _principalDetailService.GetByCurrentPrincipal();
            Expression<Func<Exchange, bool>> filterPredicate = Exchange
            .QPrincipalDetailIdEquals((int)principalDetail.Id!);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Individual)
        {
            Expression<Func<Exchange, bool>> filterPredicate = Exchange
            .QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else
        {
            return predicate ?? Expressions.True<Exchange>();
        }
    }

    public PaginatedResponse<ExchangeResponse> FindAllModels(ExchangePageableParam param)
    {
        PaginatedEntity<Exchange> pEntity = FindAll(param);
        IEnumerable<ExchangeResponse> models = pEntity.Content.Select(_mapper.Map<ExchangeResponse>);

        return PaginatedResponse<ExchangeResponse>.From(pEntity, models);
    }
}
