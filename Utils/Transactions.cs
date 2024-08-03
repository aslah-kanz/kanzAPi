using System.Transactions;

namespace KanzApi.Utils;

public sealed class Transactions
{

    public static TransactionScope Create(TransactionScopeOption scopeOption)
    {
        TransactionOptions options = new()
        {
            IsolationLevel = IsolationLevel.ReadCommitted,
            Timeout = TransactionManager.DefaultTimeout
        };

        return new TransactionScope(
            scopeOption, options, TransactionScopeAsyncFlowOption.Enabled);
    }

    public static TransactionScope Create()
    {
        return Create(TransactionScopeOption.Required);
    }
}
