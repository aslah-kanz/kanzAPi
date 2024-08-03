using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using ProductEntity = KanzApi.Product.Entities.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("ProductReview", Schema = "Transaction")]
public class ProductReview : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    private int? _productId;

    [Required]
    public int? ProductId { get { return _productId; } set { _productId = value; } }

    private ProductEntity? _product;

    public virtual ProductEntity? Product { get { return _product; } set { _productId = value?.Id; _product = value; } }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

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
        }
    }

    public int? Rating { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    public virtual List<Image> Images { get; set; } = [];

    public static Expression<Func<ProductReview, bool>> QProductIdEquals(int value)
    {
        return arg => arg.ProductId == value;
    }

    public static Expression<Func<ProductReview, bool>> QPurchaseQuoteIdEquals(Guid value)
    {
        return arg => arg.PurchaseQuoteId == value;
    }

    public static Expression<Func<ProductReview, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<ProductReview, bool>> QProductSlugEquals(string value)
    {
        return arg => arg.Product!.Slug!.Equals(value);
    }

    public static Expression<Func<ProductReview, bool>> QRatingEquals(int value)
    {
        return arg => arg.Rating == value;
    }
    
    public static Expression<Func<ProductReview, bool>> QRatingsEquals(List<int> values)
    {
        return arg => values.Contains(arg.Rating!.Value);
    }

    public static Expression<Func<ProductReview, bool>> QCommentContains(string value)
    {
        return arg => arg.Comment != null
        && arg.Comment.Contains(value);
    }

    public static Expression<Func<ProductReview, bool>> QStoreIdEquals(ISet<int> value)
    {
        return arg => value.Contains((int)arg.PurchaseQuote!.StoreId!);
    }

    public static Expression<Func<ProductReview, bool>> QStoreIdEquals(int value)
    {
        return arg => arg.PurchaseQuote!.StoreId == value;
    }
}
