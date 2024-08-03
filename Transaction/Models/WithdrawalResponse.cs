namespace KanzApi.Transaction.Models;

public class WithdrawalResponse : WithdrawalRequest
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
