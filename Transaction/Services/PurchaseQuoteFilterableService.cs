using KanzApi.Account.Entities;
using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class PurchaseQuoteFilterableService(IPurchaseQuoteRepository repository,
ISessionService sessionService, IMapper mapper, IStoreOrderService storeOrderService,
IPrincipalService principalService)
: FilterableCrudService<PurchaseQuote, Guid?>(repository), IPurchaseQuoteFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IStoreOrderService _storeOrderService = storeOrderService;

    private readonly IPrincipalService _principalService = principalService;

    public PurchaseQuoteResponse GetModelById(Guid id)
    {
        PurchaseQuote entity = GetById(id);
        return _mapper.Map<PurchaseQuoteResponse>(entity);
    }

    protected override Expression<Func<PurchaseQuote, bool>> Filter(Expression<Func<PurchaseQuote, bool>>? predicate)
    {
        int? principalId = _sessionService.CurrentPrincipalId();
        if (principalId != null)
        {
            Principal principal = _principalService.GetById(principalId);
            if (principal.Type == EPrincipalType.Manufacture
            || principal.Type == EPrincipalType.Vendor)
            {
                Expression<Func<PurchaseQuote, bool>> filterPredicate = PurchaseQuote
                .QStatusNotContains(EPurchaseQuoteStatus.WaitingPayment, EPurchaseQuoteStatus.Unassigned, EPurchaseQuoteStatus.Failed)
                .And(PurchaseQuote.QPrincipalIdEquals((int)principalId)
                .Or(PurchaseQuote.QPrincipalIdsContains((int)principalId)));
                return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
            }
        }

        return predicate ?? Expressions.True<PurchaseQuote>();
    }

    public IEnumerable<PurchaseQuote> FindAllByInvoiceNumber(string invoiceNumber)
    {
        return FindAll(PurchaseQuote.QInvoiceNumberEquals(invoiceNumber), null);
    }

    public PurchaseQuoteInvoiceResponse GetInvoiceByInvoiceNumber(string invoiceNumber)
    {
        StoreOrder storeOrder = _storeOrderService.GetByInvoiceNumber(invoiceNumber);
        IEnumerable<PurchaseQuote> entities = FindAllByInvoiceNumber(invoiceNumber);

        PurchaseQuoteInvoiceResponse model = _mapper.Map<PurchaseQuoteInvoiceResponse>(storeOrder);
        foreach (PurchaseQuote entity in entities)
        {
            PurchaseQuoteInvoiceItemResponse item = _mapper.Map<PurchaseQuoteInvoiceItemResponse>(entity);
            model.Items.Add(item);

            model.NetAmount += entity.SubTotal ?? 0;
            model.PlatformCommission += entity.PlatformCommission ?? 0;
        }

        return model;
    }

    public PaginatedResponse<PurchaseQuoteInvoiceItem> FindAllInvoices(PurchaseQuoteInvoicePageableParam param)
    {
        Page page = new(param.Page, param.Size);
        PaginatedEntity<PurchaseQuoteInvoiceItem> pEntity = repository
        .FindAllInvoices(page, Filter(param.ToPredicate()), Sort.From(param));

        IEnumerable<PurchaseQuoteInvoiceItem> models = pEntity.Content
        .Select(_mapper.Map<PurchaseQuoteInvoiceItem>);

        return PaginatedResponse<PurchaseQuoteInvoiceItem>.From(pEntity, models);
    }


    public PaginatedResponse<PurchaseQuoteResponse> FindAllModels(PurchaseQuotePageableParam param)
    {
        PaginatedEntity<PurchaseQuote> pEntity = FindAll(param);
        IEnumerable<PurchaseQuoteResponse> models = pEntity.Content.Select(_mapper.Map<PurchaseQuoteResponse>);

        return PaginatedResponse<PurchaseQuoteResponse>.From(pEntity, models);
    }
}
