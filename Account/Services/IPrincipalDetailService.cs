using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrincipalDetailService : ICrudService<PrincipalDetail, int?>
{

    CompanyMemberResponse EditMember(int principalId, CompanyMemberRequest request);

    CompanyMemberResponse DisableMember(int principalId);

    PrincipalDetail GetByPrincipalId(int principalId);
}
