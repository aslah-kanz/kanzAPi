using KanzApi.Common.Models;
using KanzApi.Common.Services;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;
using KanzApi.Transaction.Models.Param;

namespace KanzApi.Transaction.Services;

public interface IWithdrawalFilterableService : IFilterableCrudService<Withdraw, int?>
{
    WithdrawalResponse Add(WithdrawalRequest request);

    WithdrawalResponse Edit(int Id, WithdrawalRequest request);

    PaginatedResponse<WithdrawalResponse> FindAllModels(WithdrawalPageableParam param);

    decimal GetAmount();
}
