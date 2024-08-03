using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("Notification", Schema = "Account")]
public class Notification : CommonEntity<Guid?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid? Id { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Message { get; set; }

    public List<string>? MessageArgs { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public ENotificationType? Type { get; set; }

    [MaxLength(65)]
    public string? ReferenceId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReadAt { get; set; }

    public static Expression<Func<Notification, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<Notification, bool>> QUnread()
    {
        return arg => arg.ReadAt == null;
    }
}
