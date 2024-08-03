using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Repositories;
using KanzApi.Common.Services;
using MapsterMapper;

namespace KanzApi.Account.Services;

public class PrincipalDetailItemService(IPrincipalDetailItemRepository repository, IMapper mapper)
: CrudService<PrincipalDetailItem, int?>(repository), IPrincipalDetailItemService
{

    private readonly IMapper _mapper = mapper;

    public PrincipalDetailItem Add(PrincipalDetail principalDetail, PrincipalDetailItemRequest request)
    {
        PrincipalDetailItem entity = _mapper.Map<PrincipalDetailItem>(request);
        entity.PrincipalDetail = principalDetail;
        return Add(entity);
    }
}
