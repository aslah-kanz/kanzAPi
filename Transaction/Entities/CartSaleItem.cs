using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Product.Entities;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Transaction.Entities;

[Table("CartSaleItem", Schema = "Transaction")]
public class CartSaleItem : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _cartId;

    [Required]
    public int? CartId { get { return _cartId; } set { _cartId = value; } }

    private Cart? _cart;

    public virtual Cart? Cart

    { get { return _cart; } set { _cartId = value?.Id; _cart = value; } }

    private long? _saleItemId;

    [Required]
    public long? SaleItemId { get { return _saleItemId; } set { _saleItemId = value; } }

    private SaleItem? _saleItem;

    public virtual SaleItem? SaleItem
    {
        get { return _saleItem; }
        set
        {
            _saleItemId = value?.Id;
            _productId = value?.ProductId;
            _storeId = value?.StoreId;
            _minPrice = value?.MinPrice;
            _maxPrice = value?.MaxPrice;
            _discountPrice = value?.DiscountPrice;
            _stock = value?.Stock;
            _minOrderQuantity = value?.MinOrderQuantity;
            _maxOrderQuantity = value?.MaxOrderQuantity;
            _saleItem = value;
            _vendorSku = value?.VendorSku;
        }
    }

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

    { get { return _product; } set { _productId = value?.Id; _product = value; } }

    private decimal? _minPrice;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MinPrice { get { return _minPrice; } set { _minPrice = value; } }

    private decimal? _maxPrice;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MaxPrice { get { return _maxPrice; } set { _maxPrice = value; } }

    private decimal? _discountPrice;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get { return _discountPrice; } set { _discountPrice = value; } }

    private int? _stock;

    [Required]
    public int? Stock { get { return _stock; } set { _stock = value; } }

    private int? _minOrderQuantity;

    public int? MinOrderQuantity { get { return _minOrderQuantity; } set { _minOrderQuantity = value; } }

    private int? _maxOrderQuantity;

    public int? MaxOrderQuantity { get { return _maxOrderQuantity; } set { _maxOrderQuantity = value; } }

    private string? _vendorSku;

    public string? VendorSku { get { return _vendorSku; } set { _vendorSku = value; } }
}
