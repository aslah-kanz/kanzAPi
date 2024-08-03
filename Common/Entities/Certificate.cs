using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Entities;

[Table("Certificate", Schema = "Common")]
public class Certificate : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Title { get; set; }

    [MaxLength(100)]
    public string? Slug { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

    public static Expression<Func<Certificate, bool>> QTitleContains(string value)
    {
        return arg
        => (arg.Title!.En != null && arg.Title.En.Contains(value))
        || (arg.Title.Ar != null && arg.Title.Ar.Contains(value));
    }
}
