using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Repositories;
using KanzApi.Common.Exceptions;
using KanzApi.Common.Services;
using MapsterMapper;

namespace KanzApi.Account.Services;

public class PrincipalDetailService(IPrincipalDetailRepository repository,
IMapper mapper, IPrincipalService principalService)
: CrudService<PrincipalDetail, int?>(repository), IPrincipalDetailService
{

    private readonly IMapper _mapper = mapper;

    private readonly IPrincipalService _principalService = principalService;

    public CompanyMemberResponse EditMember(int principalId, CompanyMemberRequest request)
    {
        GetByPrincipalId(principalId);

        Principal principal = _principalService.Edit(principalId, request);

        return _mapper.Map<CompanyMemberResponse>(principal);
    }

    public CompanyMemberResponse DisableMember(int principalId)
    {
        Principal currentPrincipal = _principalService.GetCurrent();
        PrincipalDetail principalDetail = GetByPrincipalId(principalId);
        if (currentPrincipal != null
        && principalDetail.PrincipalId == currentPrincipal.Id
        && currentPrincipal.Id != principalId)
        {
            Principal principal = _principalService.Disable(principalId);
            return _mapper.Map<CompanyMemberResponse>(principal);
        }
        throw new PrincipalNotAllowedException();
    }

    public PrincipalDetail GetByPrincipalId(int principalId)
    {
        return _repository.FindByPredicate(PrincipalDetail.QPrincipalIdsContains(principalId))
        ?? throw EntityNotFoundException.From(typeof(PrincipalDetail), "Principal ID", principalId);
    }
}
