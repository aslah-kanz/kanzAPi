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

public class RefundFilterableService(IRefundRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService,
IPrincipalDetailFilterableService principalDetailService, IImageService imageService, IStoreFilterableService storeService)
: FilterableCrudService<Refund, Guid?>(repository), IRefundFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IImageService _imageService = imageService;

    private readonly IPrincipalService _principalService = principalService;

    private readonly IPrincipalDetailFilterableService _principalDetailService = principalDetailService;

    private readonly IStoreFilterableService _storeService = storeService;

    private string GenerateNumber(int principalId)
    {
        string prefix = "RF";
        string currentDateTime = DateTime.Now.ToString("yyMMdd-HHmmss");
        return $"{prefix}-{currentDateTime}-{principalId}";
    }

    public RefundResponse Add(RefundRequest request)
    {
        using TransactionScope scope = Transactions.Create();

        Refund? entity = FindByPurchaseQuoteId((Guid)request.PurchaseQuote!);
        if (entity == null)
        {
            entity = _mapper.Map<Refund>(request);

            Principal principal = _principalService.GetCurrent();
            entity.Principal = principal;

            PurchaseQuote purchaseQuote = entity.PurchaseQuote!;
            if (entity.PurchaseQuote!.CustomerOrder!.PrincipalId != principal.Id)
            {
                throw new PrincipalNotAllowedException();
            }

            if (purchaseQuote.Status != EPurchaseQuoteStatus.Delivered)
            {
                throw new InvalidRefundPurchaseQuoteStatusException((Guid)entity.PurchaseQuoteId!);
            }

            if (!DateUtils.SetIsExchangeableIsRefundable(purchaseQuote.UpdatedAt))
            {
                throw new RefundTimeExpiredException((Guid)entity.PurchaseQuoteId!);
            }

            entity.PrincipalDetail = _principalDetailService.FindByCurrentPrincipal();
            entity.Number = GenerateNumber((int)entity.PrincipalId!);

            foreach (IFormFile file in request.Files!)
            {
                Image image = _imageService.AddWithRandomName(file, EImageGroup.Refund);
                entity.Images.Add(image);
            }

            int maxQuantity = entity.PurchaseQuote?.ConfirmedQuantity ?? 0;
            if (request.Quantity <= maxQuantity)
            {
                entity.SubTotal = request.Quantity * entity.Price;
            }
            else
            {
                throw new InvalidRefundQuantityException((Guid)entity.PurchaseQuoteId!, maxQuantity);
            }

            entity = Add(entity);
        }
        else
        {
            throw new RefundAlreadyExistException((Guid)entity.Id!);
        }

        RefundResponse response = _mapper.Map<RefundResponse>(entity);

        scope.Complete();

        return response;
    }

    public RefundResponse AdminChangeStatus(Guid id,
    ERefundStatus fromStatus, ERefundStatus toStatus, string comment)
    {
        Refund entity = GetById(id);
        if (entity.Status != fromStatus)
        {
            throw new InvalidStateChangeException(
                entity.Status.ToString()!, toStatus.ToString());
        }

        entity.Status = toStatus;
        entity.AdminComment = comment;
        entity = Edit(entity);

        return _mapper.Map<RefundResponse>(entity);
    }

    public RefundResponse AdminApprove(Guid id, string comment)
    {
        return AdminChangeStatus(id, ERefundStatus.Pending, ERefundStatus.Reviewed, comment!);
    }

    public RefundResponse AdminReject(Guid id, string comment)
    {
        return AdminChangeStatus(id, ERefundStatus.Pending, ERefundStatus.Rejected, comment!);
    }

    public RefundResponse VendorChangeStatus(Guid id,
    ERefundStatus fromStatus, ERefundStatus toStatus, string comment)
    {
        Refund entity = GetById(id);
        if (entity.Status != fromStatus)
        {
            throw new InvalidStateChangeException(entity.Status.ToString()!, toStatus.ToString());
        }

        entity.Status = toStatus;
        entity.VendorComment = comment;
        entity = Edit(entity);

        return _mapper.Map<RefundResponse>(entity);
    }

    public RefundResponse VendorApprove(Guid id, string comment)
    {
        return VendorChangeStatus(id, ERefundStatus.Reviewed, ERefundStatus.Approved, comment!);
    }

    public RefundResponse VendorReject(Guid id, string comment)
    {
        return VendorChangeStatus(id, ERefundStatus.Reviewed, ERefundStatus.Rejected, comment!);
    }

    public RefundResponse RemoveModelById(Guid id)
    {
        Refund entity = GetById(id);
        entity.Status = ERefundStatus.Canceled;

        return _mapper.Map<RefundResponse>(entity);
    }

    public RefundResponse GetModelById(Guid id)
    {
        Refund entity = GetById(id);
        return _mapper.Map<RefundResponse>(entity);
    }

    public AdminRefundResponse GetAdminModelById(Guid id)
    {
        Refund entity = GetById(id);
        return _mapper.Map<AdminRefundResponse>(entity);
    }

    public Refund? FindByPurchaseQuoteId(Guid purchaseQuoteId)
    {
        return _repository.FindByPredicate(Filter(
            Refund.QPurchaseQuoteIdEquals(purchaseQuoteId).And(Refund.QStatusNotEquals(ERefundStatus.Rejected))));
    }

    protected override Expression<Func<Refund, bool>> Filter(Expression<Func<Refund, bool>>? predicate)
    {
        Principal principal = _principalService.GetCurrent();
        if (principal.Type == EPrincipalType.Vendor)
        {
            List<int> storeIds = _storeService.FindAll().Select(e => (int)e.Id!).ToList();
            Expression<Func<Refund, bool>> filterPredicate = Refund
            .QPurchaseQuoteStoreIdsEquals(storeIds).And(Refund.QStatusNotEquals(ERefundStatus.Pending));
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Company)
        {
            PrincipalDetail principalDetail = _principalDetailService.GetByCurrentPrincipal();
            Expression<Func<Refund, bool>> filterPredicate = Refund
            .QPrincipalDetailIdEquals((int)principalDetail.Id!);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else if (principal.Type == EPrincipalType.Individual)
        {
            Expression<Func<Refund, bool>> filterPredicate = Refund
            .QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
            return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
        }
        else
        {
            return predicate ?? Expressions.True<Refund>();
        }
    }

    public PaginatedResponse<RefundResponse> FindAllModels(RefundPageableParam param)
    {
        PaginatedEntity<Refund> pEntity = FindAll(param);
        IEnumerable<RefundResponse> models = pEntity.Content.Select(_mapper.Map<RefundResponse>);

        return PaginatedResponse<RefundResponse>.From(pEntity, models);
    }

    public PaginatedResponse<AdminRefundResponse> FindAllAdminModels(AdminRefundPageableParam param)
    {
        PaginatedEntity<Refund> pEntity = FindAll(param);
        IEnumerable<AdminRefundResponse> models = pEntity.Content.Select(_mapper.Map<AdminRefundResponse>);

        return PaginatedResponse<AdminRefundResponse>.From(pEntity, models);
    }
}
