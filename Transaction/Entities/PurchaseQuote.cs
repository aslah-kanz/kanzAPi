using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("PurchaseQuote", Schema = "Transaction")]
public class PurchaseQuote : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    public string? VendorSku { get; set; }

    private Guid? _customerOrderId;

    [Required]
    public Guid? CustomerOrderId { get { return _customerOrderId; } set { _customerOrderId = value; } }

    private CustomerOrder? _customerOrder;

    public virtual CustomerOrder? CustomerOrder
    { get { return _customerOrder; } set { _customerOrderId = value?.Id; _customerOrder = value; } }

    private Guid? _customerOrderItemId;

    [Required]
    public Guid? CustomerOrderItemId { get { return _customerOrderItemId; } set { _customerOrderItemId = value; } }

    private CustomerOrderItem? _customerOrderItem;

    public virtual CustomerOrderItem? CustomerOrderItem
    { get { return _customerOrderItem; } set { _customerOrderItemId = value?.Id; _customerOrderItem = value; } }

    private Guid? _storeOrderId;

    [Required]
    public Guid? StoreOrderId { get { return _storeOrderId; } set { _storeOrderId = value; } }

    private StoreOrder? _storeOrder;

    public virtual StoreOrder? StoreOrder
    { get { return _storeOrder; } set { _storeOrderId = value?.Id; _storeOrder = value; } }

    public int? CartId { get; set; }

    public long? SaleItemId { get; set; }

    private int? _storeId;

    [Required]
    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    private Store? _store;

    public virtual Store? Store
    { get { return _store; } set { _storeId = value?.Id; _store = value; } }

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
    public int? RequestedQuantity { get; set; }

    public int? ConfirmedQuantity { get; set; }

    [Required]
    public int? TotalRequestedQuantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    public decimal? _minPrice;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MinPrice { get { return _minPrice; } set { _minPrice = value; } }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MaxPrice { get; set; }

    public decimal? _discountPrice;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get { return _discountPrice; } set { _discountPrice = value; } }

    [Required]
    public int? Stock { get; set; } = 0;

    public int? MinOrderQuantity { get; set; }

    public int? MaxOrderQuantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Tax { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? PlatformCommission { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EPurchaseQuoteStatus? Status { get; set; } = EPurchaseQuoteStatus.WaitingPayment;

    public decimal OriginalPrice { get { return (decimal)(_discountPrice ?? _minPrice)!; } }

    public static Expression<Func<PurchaseQuote, bool>> QIdNotEquals(Guid value)
    {
        return arg => arg.Id != value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QCustomerOrderIdEquals(Guid value)
    {
        return arg => arg.CustomerOrderId == value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QStoreOrderIdEquals(Guid value)
    {
        return arg => arg.StoreOrderId == value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QStoreIdEquals(int value)
    {
        return arg => arg.StoreId == value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QPurchaseQuoteCodeEquals(string value)
    {
        return arg => arg.Code == value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QPurchaseQuoteCodeContains(string value)
    {
        return arg => arg.Code != null && arg.Code.Contains(value);
    }

    public static Expression<Func<PurchaseQuote, bool>> QInvoiceNumberEquals(string value)
    {
        return arg => arg.StoreOrder!.InvoiceNumber != null && arg.StoreOrder!.InvoiceNumber.Equals(value);
    }

    public static Expression<Func<PurchaseQuote, bool>> QInvoiceNumberContains(string value)
    {
        return arg => arg.StoreOrder!.InvoiceNumber != null && arg.StoreOrder!.InvoiceNumber.Contains(value);
    }

    public static Expression<Func<PurchaseQuote, bool>> QStatusEquals(EPurchaseQuoteStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QStatusNotEquals(EPurchaseQuoteStatus value)
    {
        return arg => arg.Status != value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QStatusNotContains(params EPurchaseQuoteStatus[] value)
    {
        return arg => !value.Any(p => p == arg.Status);
    }

    public static Expression<Func<PurchaseQuote, bool>> QStatusNotContains(List<EPurchaseQuoteStatus> value)
    {
        return arg => !value.Contains((EPurchaseQuoteStatus)arg.Status!);
    }

    public static Expression<Func<PurchaseQuote, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.Store!.PrincipalId == value;
    }

    public static Expression<Func<PurchaseQuote, bool>> QPrincipalIdsContains(int value)
    {
        return arg => arg.Store!.Principals.Any(p => value == ((int)p.Id!));
    }
}
