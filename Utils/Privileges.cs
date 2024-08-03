namespace KanzApi.Utils;

public sealed class Privileges
{

    public const string RequestOtp = "RequestOtp";
    public const string GenerateToken = "GenerateToken";
    public const string DeliverCallback = "DeliverCallback";
    public const string AddPrincipal = "AddPrincipal";
    public const string EditPrincipal = "EditPrincipal";
    public const string RemovePrincipal = "RemovePrincipal";
    public const string ViewPrincipal = "ViewPrincipal";
    public const string CancelOrder = "CancelOrder";
    public const string CompleteOrder = "CompleteOrder";
    public const string DeleteOrder = "DeleteOrder";
    public const string ViewOrder = "ViewOrder";

    public static ISet<string> ToSet()
    {
        return new SortedSet<string>
        {
            RequestOtp,
            GenerateToken,
            DeliverCallback,
            AddPrincipal,
            EditPrincipal,
            RemovePrincipal,
            ViewPrincipal,
            CancelOrder,
            CompleteOrder,
            DeleteOrder,
            ViewOrder
        };
    }
}
