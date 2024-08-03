using KanzApi.Account.Entities;

namespace KanzApi.Transaction.Models;

public class WithdrawalRequest
{
    public decimal Amount { get; set; }
    public string? Comment { get; set; }
    public string? Status { get; set; }
    public string? BankName { get; set; }
    public string? BankHolder { get; set; }
    public string? AccountNumber { get; set; }
    public string? WithdrawMethod { get; set; }
}

public class WithdrawaUpdateStatusRequest
{
    public EWithdrawStatus Status { get; set; }
}
