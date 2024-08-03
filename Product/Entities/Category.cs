using KanzApi.Common.Entities;
using KanzApi.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Product.Entities;

[Table("Category", Schema = "Product")]
[Index(nameof(Slug), IsUnique = true)]
public class Category : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _parentId;

    public int? ParentId { get { return _parentId; } set { _parentId = value; } }

    private Category? _parent;

    public virtual Category? Parent
    { get { return _parent; } set { _parentId = value?.Id; _parent = value; } }

    [MaxLength(8)]
    public string? Code { get; set; }

    private LocalizableString? _name;

    [Required]
    public LocalizableString? Name { get { return _name; } set { _name = value; } }

    [Required]
    [MaxLength(100)]
    public string? Slug { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

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

    [Required]
    [MaxLength(255)]
    public string? Path { get; set; }

    public virtual ISet<Category> Children { get; set; } = new HashSet<Category>();

    public virtual ISet<Product> Products { get; set; } = new HashSet<Product>();

    public virtual ISet<Brand> Brands { get; set; } = new HashSet<Brand>();

    public string StringifiedPath()
    {
        string? path = _parentId != null ? Parent!.StringifiedPath() : null;
        return path != null ? path + " > " + _name!.En : _name!.En!;
    }

    public LinkedList<Category> LinkedPath()
    {
        LinkedList<Category> parents = _parentId != null ? Parent!.LinkedPath() : new();
        parents.AddLast(this);

        return parents;
    }

    public static Expression<Func<Category, bool>> QParentEquals(int value)
    {
        return arg => arg.ParentId == value;
    }

    public static Expression<Func<Category, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }

    public static Expression<Func<Category, bool>> QSlugEquals(string value)
    {
        return arg => arg.Slug!.Equals(value);
    }

    public static Expression<Func<Category, bool>> QDescriptionContains(string value)
    {
        return arg => arg.Description != null
        && ((arg.Description.En != null && arg.Description.En.Contains(value))
        || (arg.Description.Ar != null && arg.Description.Ar.Contains(value)));
    }

    public static Expression<Func<Category, bool>> QShowAtHomePageEquals(bool value)
    {
        return arg => arg.ShowAtHomePage == value;
    }

    public static Expression<Func<Category, bool>> QStatusEquals(ERecordStatus value)
    {
        return arg => arg.Status.Equals(value);
    }

    public static Expression<Func<Category, bool>> QBrandIdsContains(ISet<int> value)
    {
        return arg => arg.Brands.Any(p => value.Contains((int)p.Id!));
    }
}
