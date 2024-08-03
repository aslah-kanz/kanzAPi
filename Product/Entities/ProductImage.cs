using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Product.Entities;

[Table("ProductImage", Schema = "Product")]
[Index(nameof(ProductId), nameof(ImageId), IsUnique = true)]
public class ProductImage : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _productId;

    [Required]
    public int? ProductId { get { return _productId; } set { _productId = value; } }

    private Product? _product;

    public virtual Product? Product
    { get { return _product; } set { _productId = value?.Id; _product = value; } }

    private long? _imageId;

    [Required]
    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    [Required]
    public int? SortOrder { get; set; } = 0;

    public static Expression<Func<ProductImage, bool>> QProductIdEquals(int value)
    {
        return arg => arg.ProductId == value;
    }
}
