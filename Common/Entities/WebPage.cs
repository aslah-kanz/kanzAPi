using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Entities;

[Table("WebPage", Schema = "Common")]
[Index(nameof(Slug), IsUnique = true)]
public class WebPage : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Slug { get; set; }

    [Required]
    public LocalizableString? Title { get; set; }

    [Required]
    public bool? ShowAtHomePage { get; set; } = false;

    [Required]
    public List<LocalizableString> Contents { get; set; } = [];

    [MaxLength(65)]
    public string? MetaKeyword { get; set; }

    [MaxLength(160)]
    public string? MetaDescription { get; set; }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

    public static Expression<Func<WebPage, bool>> QTitleContains(string value)
    {
        return arg
        => (arg.Title!.En != null && arg.Title.En.Contains(value))
        || (arg.Title.Ar != null && arg.Title.Ar.Contains(value));
    }

    public static Expression<Func<WebPage, bool>> QSlugEquals(string value)
    {
        return arg => arg.Slug!.Equals(value);
    }

    public static Expression<Func<WebPage, bool>> QSlugContains(string value)
    {
        return arg => arg.Slug != null && arg.Slug.Contains(value);
    }
}
