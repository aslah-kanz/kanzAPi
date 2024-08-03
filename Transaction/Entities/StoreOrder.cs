using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("StoreOrder", Schema = "Transaction")]
[Index(nameof(InvoiceNumber), IsUnique = true)]
public class StoreOrder : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? InvoiceNumber { get; set; }

    public int? DeliveryId { get; set; }

    private Guid? _customerOrderId;

    [Required]
    public Guid? CustomerOrderId { get { return _customerOrderId; } set { _customerOrderId = value; } }

    private CustomerOrder? _customerOrder;

    public virtual CustomerOrder? CustomerOrder
    { get { return _customerOrder; } set { _customerOrderId = value?.Id; _customerOrder = value; } }

    private int? _storeId;

    [Required]
    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    private Store? _store;

    public virtual Store? Store
    { get { return _store; } set { _storeId = value?.Id; _store = value; } }

    public int? PackageCount { get; set; }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EPurchaseQuoteStatus? PurchaseQuoteStatus { get; set; } = EPurchaseQuoteStatus.WaitingPayment;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? DeliveryCost { get; set; } = 0;

    [Required]
    public int? ProductCount { get; set; } = 0;

    public static Expression<Func<StoreOrder, bool>> QInvoiceNumberEquals(string value)
    {
        return arg => arg.InvoiceNumber != null && arg.InvoiceNumber.Equals(value);
    }

    public static Expression<Func<StoreOrder, bool>> QInvoiceNumberContains(string value)
    {
        return arg => arg.InvoiceNumber != null && arg.InvoiceNumber.Contains(value);
    }

    public static Expression<Func<StoreOrder, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.Store!.PrincipalId == value;
    }

    public static Expression<Func<StoreOrder, bool>> QPrincipalIdsContains(int value)
    {
        return arg => arg.Store!.Principals.Any(p => value == ((int)p.Id!));
    }
}
