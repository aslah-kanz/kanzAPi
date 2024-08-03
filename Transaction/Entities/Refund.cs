using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

using ProductEntity = KanzApi.Product.Entities.Product;

namespace KanzApi.Transaction.Entities;

[Table("Refund", Schema = "Transaction")]
public class Refund : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    [MaxLength(100)]
    public string? Number { get; set; }

    private Guid? _purchaseQuoteId;

    [Required]
    public Guid? PurchaseQuoteId { get { return _purchaseQuoteId; } set { _purchaseQuoteId = value; } }

    private PurchaseQuote? _purchaseQuote;

    public virtual PurchaseQuote? PurchaseQuote
    {
        get { return _purchaseQuote; }
        set
        {
            _purchaseQuoteId = value?.Id;
            _purchaseQuote = value;
            _product = value?.Product;
            _price = value?.Price;
            _store = value?.Store;
        }
    }

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

    private int? _storeId;

    [Required]
    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    private Store? _store;

    public virtual Store? Store
    {
        get { return _store; }
        set
        {
            if (value != null) _storeId = value.Id;
            _store = value;
        }
    }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

    private int? _principalDetailId;

    public int? PrincipalDetailId { get { return _principalDetailId; } set { _principalDetailId = value; } }

    private PrincipalDetail? _principalDetail;

    public virtual PrincipalDetail? PrincipalDetail
    { get { return _principalDetail; } set { _principalDetailId = value?.Id; _principalDetail = value; } }

    [Required]
    public int? Quantity { get; set; }

    private decimal? _price;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get { return _price; } set { _price = value; } }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? SubTotal { get; set; }

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public ERefundStatus? Status { get; set; } = ERefundStatus.Pending;

    [MaxLength(255)]
    public string? AdminComment { get; set; }

    [MaxLength(255)]
    public string? VendorComment { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    public virtual List<Image> Images { get; set; } = [];

    public static Expression<Func<Refund, bool>> QNumberEquals(string value)
    {
        return arg => arg.Number == value;
    }

    public static Expression<Func<Refund, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<Refund, bool>> QCustomerOrderPrincipalIdEquals(int value)
    {
        return arg => arg.PurchaseQuote!.CustomerOrder!.PrincipalId == value;
    }

    public static Expression<Func<Refund, bool>> QPrincipalDetailIdEquals(int value)
    {
        return arg => arg.PrincipalDetailId == value;
    }

    public static Expression<Func<Refund, bool>> QPurchaseQuoteIdEquals(Guid value)
    {
        return arg => arg.PurchaseQuoteId == value;
    }

    public static Expression<Func<Refund, bool>> QStatusEquals(ERefundStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<Refund, bool>> QStatusNotEquals(ERefundStatus value)
    {
        return arg => arg.Status != value;
    }

    public static Expression<Func<Refund, bool>> QPurchaseQuoteStoreIdsEquals(List<int> value)
    {
        return arg => value.Contains((int)arg.PurchaseQuote!.StoreId!);
    }

    public static Expression<Func<Refund, bool>> QStoreIdEquals(int value)
    {
        return arg => arg.StoreId == value;
    }
}
