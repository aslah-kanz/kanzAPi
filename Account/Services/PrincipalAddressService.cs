using KanzApi.Account.Entities;
using KanzApi.Account.Repositories;
using KanzApi.Common.Services;
using KanzApi.Extensions;

namespace KanzApi.Account.Services;

public class PrincipalAddressService(IPrincipalAddressRepository repository)
: CrudService<PrincipalAddress, int?>(repository), IPrincipalAddressService
{

    public override PrincipalAddress Add(PrincipalAddress entity)
    {
        throw new NotSupportedException();
    }

    public override PrincipalAddress Edit(PrincipalAddress entity)
    {
        throw new NotSupportedException();
    }

    public override PrincipalAddress Remove(PrincipalAddress entity)
    {
        throw new NotSupportedException();
    }

    public PrincipalAddress GetDefaultByPrincipalId(int principalId)
    {
        return FindByPredicate(PrincipalAddress.QPrincipalIdEquals(principalId)
            .And(PrincipalAddress.QDefaultAddressEquals(true)))
        ?? throw EntityNotFoundException.From(typeof(PrincipalAddress),
        new Dictionary<string, object?> { { "Principal ID", principalId }, { "Default Address", true } });
    }
}
