using KanzApi.Common.Entities;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("CustomerOrderItem", Schema = "Transaction")]
public class CustomerOrderItem : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    private Guid? _customerOrderId;

    [Required]
    public Guid? CustomerOrderId { get { return _customerOrderId; } set { _customerOrderId = value; } }

    private CustomerOrder? _customerOrder;

    public virtual CustomerOrder? CustomerOrder
    { get { return _customerOrder; } set { _customerOrderId = value?.Id; _customerOrder = value; } }

    public int? CartId { get; set; }

    private int? _productId;

    [Required]
    public int? ProductId { get { return _productId; } set { _productId = value; } }

    private ProductEntity? _product;

    public virtual ProductEntity? Product
    {
        get { return _product; }
        set
        {
            if (value != null) _productId = value.Id;
            _product = value;
        }
    }

    [Required]
    public int? Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? SubTotal { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public ECustomerOrderItemStatus? Status { get; set; } = ECustomerOrderItemStatus.Pending;

    [NotMapped]
    public decimal EstimatedDeliveryCost { get; set; } = 0;

    public virtual List<PurchaseQuote> PurchaseQuotes { get; set; } = [];

    public static Expression<Func<CustomerOrderItem, bool>> QCustomerOrderIdEquals(Guid? value)
    {
        return arg => arg.CustomerOrderId == value;
    }
}
