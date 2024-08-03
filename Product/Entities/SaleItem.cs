using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Extensions;
using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Product.Entities;

[Table("SaleItem", Schema = "Product")]
[Index(nameof(ProductId), nameof(StoreId), IsUnique = true)]
public class SaleItem : CommonEntity<long?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    private int? _productId;

    [Required]
    public int? ProductId { get { return _productId; } set { _productId = value; } }

    private Product? _product;

    public virtual Product? Product
    {
        get { return _product; }
        set
        {
            if (value != null) _productId = value.Id;
            _product = value;
        }
    }

    public string? VendorSku { get; set; }

    public string? BcId { get; set; }

    [Required]
    public int? Stock { get; set; } = 0;

    [Required]
    public int? ReservedStock { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MinPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? MaxPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get; set; } = 0;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? OriginalPrice { get; set; }

    public int? MinOrderQuantity { get; set; }

    public int? MaxOrderQuantity { get; set; }

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

    [Required]
    public bool? Enabled { get; set; } = true;

    [Required]
    [MaxLength(30)]
    [Column(TypeName = "nvarchar(30)")]
    public EActivableStatus? Status { get; set; } = EActivableStatus.Active;

    public static Expression<Func<SaleItem, bool>> QProductIdEquals(int value)
    {
        return arg => arg.ProductId == value;
    }

    public static Expression<Func<SaleItem, bool>> QIdNotEquals(long value)
    {
        return arg => arg.Id != value;
    }

    public static Expression<Func<SaleItem, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.Store!.PrincipalId == value;
    }

    public static Expression<Func<SaleItem, bool>> QStoreIdEquals(int value)
    {
        return arg => arg.StoreId == value;
    }

    public static Expression<Func<SaleItem, bool>> QPrincipalIdsContains(int value)
    {
        return arg => arg.Store!.Principals.Any(p => value == ((int)p.Id!));
    }

    public static Expression<Func<SaleItem, bool>> QEnabledEquals(bool value)
    {
        return arg => arg.Enabled == value;
    }

    public static Expression<Func<SaleItem, bool>> QStockNotEmpty()
    {
        return arg => arg.Stock > 0;
    }

    public static Expression<Func<SaleItem, bool>> QVendorSkuContains(string value)
    {
        return arg => arg.VendorSku != null && arg.VendorSku.Contains(value);
    }

    public static Expression<Func<SaleItem, bool>> QBrandIdEquals(int value)
    {
        return arg => arg.Product!.BrandId == value;
    }

    public static Expression<Func<SaleItem, bool>> QMpnStartsWith(string value)
    {
        return arg => arg.Product!.Mpn != null && arg.Product!.Mpn.StartsWith(value);
    }

    public static Expression<Func<SaleItem, bool>> QCategorySlugsContains(ISet<string> value)
    {
        return arg => arg.Product!.Categories.Any(p => value.Contains(p.Slug!));
    }

    public static Expression<Func<SaleItem, bool>> QStockLessThanOrEquals(int value)
    {
        return arg => arg.Stock <= value;
    }

    public static Expression<Func<SaleItem, bool>> QStockGreaterThanOrEquals(int value)
    {
        return arg => arg.Stock >= value;
    }

    public static Expression<Func<SaleItem, bool>> QStockBetween(int min, int max)
    {
        return QStockGreaterThanOrEquals(min).And(QStockLessThanOrEquals(max));
    }

    public static Expression<Func<SaleItem, bool>> QStatusEquals(EActivableStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<SaleItem, bool>> QCountryEquals(string value)
    {
        return arg => arg.Store!.Country == value;
    }
}
