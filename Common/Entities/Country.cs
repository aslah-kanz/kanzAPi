using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Entities;

[Table("Country", Schema = "Common")]
[Index(nameof(PhoneCode), IsUnique = true)]
public class Country : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [Required]
    [MaxLength(2)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Name { get; set; }

    [Required]
    public int? PhoneCode { get; set; }

    [Required]
    public int? PhoneStartNumber { get; set; }

    [Required]
    public int? PhoneMinLength { get; set; }

    [Required]
    public int? PhoneMaxLength { get; set; }

    private long? _imageId;
    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;
    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    public static Expression<Func<Country, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }
}
