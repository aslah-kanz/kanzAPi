using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Product.Entities;

[Table("Brand", Schema = "Product")]
[Index(nameof(Slug), IsUnique = true)]
[Index(nameof(NameEn), IsUnique = true)]
public class Brand : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    private LocalizableString? _name;

    [Required]
    public LocalizableString? Name
    {
        get { return _name; }
        set
        {
            _name = value;
            _nameEn = value?.En;
        }
    }

    private string? _nameEn;

    [Required]
    [MaxLength(100)]
    public string? NameEn { get { return _nameEn; } set { _nameEn = value; } }

    [Required]
    [MaxLength(100)]
    public string? Slug { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    private long? _bwImageId;

    public long? BwImageId { get { return _bwImageId; } set { _bwImageId = value; } }

    private Image? _bwImage;

    public virtual Image? BwImage
    { get { return _bwImage; } set { _bwImageId = value?.Id; _bwImage = value; } }

    public LocalizableString? Description { get; set; }

    [MaxLength(65)]
    public string? MetaKeyword { get; set; }

    [MaxLength(160)]
    public string? MetaDescription { get; set; }

    [Required]
    public bool? ShowAtHomePage { get; set; } = false;

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public ERecordStatus? Status { get; set; } = ERecordStatus.Draft;

    public virtual ISet<Category> Categories { get; set; } = new HashSet<Category>();

    public static Expression<Func<Brand, bool>> QNameStartsWith(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.StartsWith(value))
        || (arg.Name.Ar != null && arg.Name.Ar.StartsWith(value));
    }

    public static Expression<Func<Brand, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }

    public static Expression<Func<Brand, bool>> QSlugEquals(string value)
    {
        return arg => arg.Slug!.Equals(value);
    }

    public static Expression<Func<Brand, bool>> QShowAtHomePageEquals(bool value)
    {
        return arg => arg.ShowAtHomePage == value;
    }

    public static Expression<Func<Brand, bool>> QStatusEquals(ERecordStatus value)
    {
        return arg => arg.Status.Equals(value);
    }

    public static Expression<Func<Brand, bool>> QHasCategories()
    {
        return arg => arg.Categories.Any();
    }

    public static Expression<Func<Brand, bool>> QCategorySlugsContains(ISet<string> value)
    {
        return arg => arg.Categories.Any(p => value.Contains(p.Slug!));
    }
}
