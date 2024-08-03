using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Entities;

[Table("Language", Schema = "Common")]
[Index(nameof(Code), IsUnique = true)]
public class Language : CommonEntity<int?>
{
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }
    
    [Required]
    public LocalizableString? Name { get; set; }

    private long? _imageId;
    public long? ImageId { get { return _imageId; } set { _imageId = value; } }
    
    private Image? _image;
    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    public static Expression<Func<Language, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }
}
