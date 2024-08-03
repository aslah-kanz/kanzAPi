using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("Cart", Schema = "Transaction")]
[Index(nameof(PrincipalId), nameof(ProductId), nameof(Price), IsUnique = true)]
public class Cart : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

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
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    [Required]
    public int? Quantity { get; set; }

    [Required]
    public int? Stock { get; set; } = 0;

    [MaxLength(255)]
    public string? Comment { get; set; }

    public virtual ISet<CartSaleItem> CartSaleItems { get; set; } = new HashSet<CartSaleItem>();

    public static Expression<Func<Cart, bool>> QProductIdEquals(long value)
    {
        return arg => arg.ProductId == value;
    }

    public static Expression<Func<Cart, bool>> QProductIdEquals(ISet<int> value)
    {
        return arg => value.Contains((int)arg.ProductId!);
    }

    public static Expression<Func<Cart, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<Cart, bool>> QPriceEquals(decimal value)
    {
        return arg => arg.Price == value;
    }
}
