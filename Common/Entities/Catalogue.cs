using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Entities;

[Table("Catalogue", Schema = "Common")]
public class Catalogue : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Title { get; set; }

    [MaxLength(100)]
    public string? Slug { get; set; }

    public LocalizableString? Description { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    private long? _documentId;

    public long? DocumentId { get { return _documentId; } set { _documentId = value; } }

    private Document? _document;

    public virtual Document? Document
    { get { return _document; } set { _documentId = value?.Id; _document = value; } }

    [MaxLength(160)]
    public string? MetaDescription { get; set; }

    [MaxLength(65)]
    public string? MetaKeyword { get; set; }

    [Required]
    public int ReadCount { get; set; } = 0;

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EPublishableStatus? Status { get; set; } = EPublishableStatus.Draft;

    public static Expression<Func<Catalogue, bool>> QTitleContains(string value)
    {
        return arg
        => (arg.Title!.En != null && arg.Title.En.Contains(value))
        || (arg.Title.Ar != null && arg.Title.Ar.Contains(value));
    }
}
