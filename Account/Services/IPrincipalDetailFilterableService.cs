using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;

namespace KanzApi.Account.Services;

public interface IPrincipalDetailFilterableService : IFilterableCrudService<PrincipalDetail, int?>
{

    PrincipalDetailResponse Add(Principal principal, PrincipalDetailRequest request);

    PrincipalDetailResponse Add(PrincipalDetailRequest request);

    CompanyMemberResponse AddMember(CompanyMemberRequest request);

    PrincipalDetailResponse Edit(int id, PrincipalDetailRequest request);

    PrincipalDetailResponse GetModelById(int id);

    PrincipalDetail? FindByCurrentPrincipal();

    PrincipalDetail GetByCurrentPrincipal();

    PaginatedResponse<PrincipalDetailResponse> FindAllModels(PrincipalDetailPageableParam param);

    PaginatedResponse<CustomerItem> FindAllCompanyMembers(CompanyMemberPageableParam param);
}
