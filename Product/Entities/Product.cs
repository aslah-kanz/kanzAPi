using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using KanzApi.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Product.Entities;

[Table("Product", Schema = "Product")]
[Index(nameof(Slug), IsUnique = true)]
public class Product : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    [MaxLength(100)]
    public string? Mpn { get; set; }

    public Gtin? Gtin { get; set; }

    [Required]
    public LocalizableString? Name { get; set; }

    [MaxLength(100)]
    public string? Slug { get; set; }

    private string? _familyCode;

    [MaxLength(60)]
    public string? FamilyCode
    {
        get { return _familyCode; }
        set
        {
            _familyCode = value;
            _groupCode ??= value;
        }
    }

    private string? _groupCode;

    [MaxLength(60)]
    public string? GroupCode { get { return _groupCode; } set { _groupCode = value; } }

    private long? _iconId;

    public long? IconId { get { return _iconId; } set { _iconId = value; } }

    private Image? _icon;

    public virtual Image? Icon
    { get { return _icon; } set { _iconId = value?.Id; _icon = value; } }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    public LocalizableString? Description { get; set; }

    private int? _brandId;

    [Required]
    public int? BrandId { get { return _brandId; } set { _brandId = value; } }

    private Brand? _brand;

    public virtual Brand? Brand
    { get { return _brand; } set { _brandId = value?.Id; _brand = value; } }

    private int? _principalDetailId;

    public int? PrincipalDetailId { get { return _principalDetailId; } set { _principalDetailId = value; } }

    private PrincipalDetail? _principalDetail;

    public virtual PrincipalDetail? PrincipalDetail
    { get { return _principalDetail; } set { _principalDetailId = value?.Id; _principalDetail = value; } }

    [Required]
    public double? Length { get; set; }

    [Required]
    public double? Width { get; set; }

    [Required]
    public double? Height { get; set; }

    [Required]
    public double? Weight { get; set; }

    [MaxLength(65)]
    public string? MetaKeyword { get; set; }

    [MaxLength(160)]
    public string? MetaDescription { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? OriginalPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? LowestPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? HighestPrice { get; set; }

    [Required]
    public bool? Sellable { get; set; } = true;

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EProductStatus? Status { get; set; } = EProductStatus.Draft;

    [MaxLength(255)]
    public string? Comment { get; set; }

    public int? SortOrder { get; set; } = 0;

    public virtual ISet<Category> Categories { get; set; } = new HashSet<Category>();

    public virtual ISet<ProductImage> Images { get; set; } = new HashSet<ProductImage>();

    public virtual ISet<ProductAttribute> Attributes { get; set; } = new HashSet<ProductAttribute>();

    public static Expression<Func<Product, bool>> QCodeStartsWith(string value)
    {
        return arg => arg.Code != null && arg.Code.StartsWith(value);
    }

    public static Expression<Func<Product, bool>> QMpnStartsWith(string value)
    {
        return arg => arg.Mpn != null && arg.Mpn.StartsWith(value);
    }

    public static Expression<Func<Product, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }

    public static Expression<Func<Product, bool>> QBrandNameStartsWith(string value)
    {
        return arg
        => (arg.Brand!.Name!.En != null && arg.Brand!.Name!.En.StartsWith(value))
        || (arg.Brand!.Name!.Ar != null && arg.Brand!.Name!.Ar.StartsWith(value));
    }

    public static Expression<Func<Product, bool>> QSlugEquals(string value)
    {
        return arg => arg.Slug!.Equals(value);
    }

    public static Expression<Func<Product, bool>> QFamilyCodeStartsWith(string value)
    {
        return arg => arg.FamilyCode != null && arg.FamilyCode.StartsWith(value);
    }

    public static Expression<Func<Product, bool>> QFamilyCodeEquals(string value)
    {
        return arg => arg.FamilyCode != null && arg.FamilyCode!.Equals(value);
    }

    public static Expression<Func<Product, bool>> QBrandIdEquals(ISet<int> value)
    {
        return arg => value.Contains((int)arg.BrandId!);
    }

    public static Expression<Func<Product, bool>> QBrandIdEquals(int value)
    {
        return arg => arg.BrandId == value;
    }

    public static Expression<Func<Product, bool>> QDescriptionContains(string value)
    {
        return arg => arg.Description != null
        && ((arg.Description.En != null && arg.Description.En.Contains(value))
        || (arg.Description.Ar != null && arg.Description.Ar.Contains(value)));
    }

    public static Expression<Func<Product, bool>> QPriceLessThanOrEquals(decimal value)
    {
        return arg => arg.HighestPrice != null ? arg.HighestPrice <= value : arg.LowestPrice <= value;
    }

    public static Expression<Func<Product, bool>> QPriceGreaterThanOrEquals(decimal value)
    {
        return arg => arg.LowestPrice >= value;
    }

    public static Expression<Func<Product, bool>> QPriceBetween(decimal min, decimal max)
    {
        return QPriceGreaterThanOrEquals(min).And(QPriceLessThanOrEquals(max));
    }

    public static Expression<Func<Product, bool>> QSellableEquals(bool value)
    {
        return arg => arg.Sellable == value;
    }

    public static Expression<Func<Product, bool>> QCategoryIdsContains(int value)
    {
        return arg => arg.Categories.Any(p => p.Id == value);
    }

    public static Expression<Func<Product, bool>> QCategorySlugsContains(ISet<string> value)
    {
        return arg => arg.Categories.Any(p => value.Contains(p.Slug!));
    }

    public static Expression<Func<Product, bool>> QStatusEquals(EProductStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<Product, bool>> QPrincipalDetailIdEquals(int value)
    {
        return arg => arg.PrincipalDetailId == value;
    }
}
