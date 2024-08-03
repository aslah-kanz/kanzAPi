using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("Inquiry", Schema = "Transaction")]
public class Inquiry : CommonEntity<int?>
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

    [Column(TypeName = "decimal(18,2)")]
    public decimal? TotalPrice { get; set; }

    public int Quantity { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EInquiryStatus? Status { get; set; } = EInquiryStatus.Pending;

    public static Expression<Func<Inquiry, bool>> QProductIdEquals(long value)
    {
        return arg => arg.ProductId == value;
    }

    public static Expression<Func<Inquiry, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }
    public static Expression<Func<Inquiry, bool>> QProductNameContains(string value)
    {
        return arg
        => (arg.Product!.Name!.En != null && arg.Product!.Name!.En.Contains(value))
        || (arg.Product!.Name!.Ar != null && arg.Product!.Name!.Ar.Contains(value));
    }

    public static Expression<Func<Inquiry, bool>> QProductSlugContains(string value)
    {
        return arg
        => arg.Product!.Slug! != null && arg.Product!.Slug!.Contains(value);
    }

    public static Expression<Func<Inquiry, bool>> QMpnContains(string value)
    {
        return arg
        => arg.Product!.Mpn != null && arg.Product!.Mpn.Contains(value);
    }
}
