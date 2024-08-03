using KanzApi.Account.Services;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Extensions;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;
using KanzApi.Transaction.Repositories;
using MapsterMapper;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Services;

public class InquiryFilterableService(IInquiryRepository repository,
ISessionService sessionService, IMapper mapper, IPrincipalService principalService)
: FilterableCrudService<Inquiry, int?>(repository), IInquiryFilterableService
{

    private readonly ISessionService _sessionService = sessionService;

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    public InquiryResponse Add(AddInquiryRequest request)
    {
        Inquiry entity = _mapper.Map<Inquiry>(request);

        entity.Principal = _principalService.GetById(_sessionService.CurrentPrincipalId() ?? 0);

        entity = Add(entity);

        return _mapper.Map<InquiryResponse>(entity);
    }

    public InquiryResponse Edit(int id, EditInquiryRequest request)
    {
        Inquiry entity = GetById(id);
        _mapper.Map(request, entity);

        entity = Edit(entity);

        return _mapper.Map<InquiryResponse>(entity);
    }

    public InquiryResponse RemoveModelById(int id)
    {
        Inquiry entity = RemoveById(id);
        return _mapper.Map<InquiryResponse>(entity);
    }

    public InquiryResponse GetModelById(int id)
    {
        Inquiry entity = GetById(id);
        return _mapper.Map<InquiryResponse>(entity);
    }

    protected override Expression<Func<Inquiry, bool>> Filter(Expression<Func<Inquiry, bool>>? predicate)
    {
        Expression<Func<Inquiry, bool>> filterPredicate = Inquiry.QPrincipalIdEquals((int)_sessionService.CurrentPrincipalId()!);
        return predicate != null ? predicate.And(filterPredicate) : filterPredicate;
    }

    public PaginatedResponse<InquiryResponse> FindAllModels(InquiryPageableParam param)
    {
        PaginatedEntity<Inquiry> pEntity = FindAll(param);
        IEnumerable<InquiryResponse> models = pEntity.Content.Select(_mapper.Map<InquiryResponse>);

        return PaginatedResponse<InquiryResponse>.From(pEntity, models);
    }

    public IEnumerable<int> FindAllProductIds()
    {
        IEnumerable<Inquiry> pEntity = FindAll();

        return pEntity.Select(e => (int)e.ProductId!);
    }
}
