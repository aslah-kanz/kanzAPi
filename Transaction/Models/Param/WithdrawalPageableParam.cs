using KanzApi.Common.Models.Param;
using KanzApi.Transaction.Entities;

namespace KanzApi.Transaction.Models.Param;

public class WithdrawalPageableParam : PageableParam<WithdrawListSort, Withdraw>
{

    public WithdrawalPageableParam() : base(WithdrawListSort.Id) { }
}
