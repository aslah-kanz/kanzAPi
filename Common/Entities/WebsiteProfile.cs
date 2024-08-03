using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Common.Entities;

[Table("WebsiteProfile", Schema = "Common")]
public class WebsiteProfile : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [MaxLength(65)]
    public string? Name { get; set; }

    [MaxLength(65)]
    public string? MetaKeyword { get; set; }

    [MaxLength(160)]
    public string? MetaDescription { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    private long? _faviconId;

    public long? FaviconId { get { return _faviconId; } set { _faviconId = value; } }

    private Image? _favicon;

    public virtual Image? Favicon
    { get { return _favicon; } set { _faviconId = value?.Id; _favicon = value; } }

    [MaxLength(160)]
    public string? Instagram { get; set; }

    [MaxLength(160)]
    public string? Twitter { get; set; }

    [MaxLength(160)]
    public string? Facebook { get; set; }

    [MaxLength(160)]
    public string? Linkedin { get; set; }

    [MaxLength(160)]
    public string? Youtube { get; set; }

    private string? _phoneNumber;

    [MaxLength(15)]
    public string? PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    public static Expression<Func<WebsiteProfile, bool>> QNameContains(string value)
    {
        return arg => arg.Name != null && arg.Name.Contains(value);
    }
}
