using KanzApi.Account.Entities;
using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IWithdrawalService : ICrudService<Withdraw, int?>
{

    WithdrawalResponse UpdateStatus(int Id, EWithdrawStatus status);

    PaginatedResponse<WithdrawalResponse> FindAllModels(WithdrawalPageableParam param);
}
