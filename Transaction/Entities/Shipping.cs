using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("Shipping", Schema = "Transaction")]
public class Shipping : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    private Guid? _customerOrderId;

    [Required]
    public Guid? CustomerOrderId { get { return _customerOrderId; } set { _customerOrderId = value; } }

    private CustomerOrder? _customerOrder;

    public virtual CustomerOrder? CustomerOrder
    { get { return _customerOrder; } set { _customerOrderId = value?.Id; _customerOrder = value; } }

    private int? _shippingMethodId;

    [Required]
    public int? ShippingMethodId { get { return _shippingMethodId; } set { _shippingMethodId = value; } }

    private ShippingMethod? _shippingMethod;

    public virtual ShippingMethod? ShippingMethod
    { get { return _shippingMethod; } set { _shippingMethodId = value?.Id; _shippingMethod = value; } }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Cost { get; set; }

    public static Expression<Func<Shipping, bool>> QCustomerOrderIdEquals(Guid? value)
    {
        return arg => arg.Id == value;
    }
}
