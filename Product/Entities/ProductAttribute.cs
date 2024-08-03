using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Product.Entities;

[Table("ProductAttribute", Schema = "Product")]
[Index(nameof(ProductId), nameof(AttributeId), IsUnique = true)]
public class ProductAttribute : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    private int? _productId;

    [Required]
    public int? ProductId { get { return _productId; } set { _productId = value; } }

    private Product? _product;

    public virtual Product? Product
    { get { return _product; } set { _productId = value?.Id; _product = value; } }

    private int? _propertyId;

    [Required]
    public int? PropertyId { get { return _propertyId; } set { _propertyId = value; } }

    private Property? _property;

    public virtual Property? Property
    { get { return _property; } set { _propertyId = value?.Id; _property = value; } }

    private int? _attributeId;

    [Required]
    public int? AttributeId { get { return _attributeId; } set { _attributeId = value; } }

    private Attribute? _attribute;

    public virtual Attribute? Attribute
    { get { return _attribute; } set { _attributeId = value?.Id; _attribute = value; } }

    [Required]
    public string? Value1En { get; set; }

    public string? Value1Ar { get; set; }

    public string? Value2En { get; set; }

    public string? Value2Ar { get; set; }

    public string? Value3En { get; set; }

    public string? Value3Ar { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    public static Expression<Func<ProductAttribute, bool>> QProductIdEquals(int value)
    {
        return arg => arg.ProductId == value;
    }
}
