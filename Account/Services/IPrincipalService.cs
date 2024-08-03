using KanzApi.Account.Entities;
using KanzApi.Account.Models;
using KanzApi.Account.Models.Param;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Security.Models;

namespace KanzApi.Account.Services;

public interface IPrincipalService : ICrudService<Principal, int?>
{

    PrincipalResponse Add(PrincipalRequest request);

    Principal Add(SignupRequest request, bool generatePassword);

    Principal Add(IMemberRequest request);

    PrincipalResponse Edit(int id, EditPrincipalRequest request);

    Principal Edit(int principalId, CompanyMemberRequest request);

    PrincipalResponse RemoveModelById(int id);

    Principal Disable(int principalId);

    Principal? FindByUsername(string username);

    Principal GetByUsernameAndPassword(string username, string password, params EPrincipalType[]? types);

    PrincipalResponse GetModelById(int id);

    Principal GetByEmail(string email);

    Principal? FindCurrent();

    Principal GetCurrent();

    Principal RequestApproval();

    PrincipalResponse Approve(int id);

    PrincipalResponse Reject(int id);

    PaginatedResponse<PrincipalResponse> FindAllModels(PrincipalPageableParam param);
}
