using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Entities;

[Table("Image", Schema = "Common")]
[Index(nameof(Group), nameof(Name), IsUnique = true)]
public class Image : CommonEntity<long?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long? Id { get; set; }

    private EImageGroup? _group;

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EImageGroup? Group { get { return _group; } set { _group = value; } }

    private string? _name;

    [Required]
    [MaxLength(100)]
    public string? Name { get { return _name; } set { _name = value; } }

    [Required]
    public int? Width { get; set; }

    [Required]
    public int? Height { get; set; }

    [Required]
    [MaxLength(20)]
    public string? Type { get; set; }

    public string ToPath()
    {
        return FileUtils.ToPath(_group?.ToString(), _name!);
    }

    public string ToUrl(string baseUrl)
    {
        return FileUtils.ToUrl(baseUrl, Constants.ImageStorageContainer, _group?.ToString(), _name!);
    }

    public static Expression<Func<Image, bool>> QGroupEquals(EImageGroup value)
    {
        return arg => arg.Group!.Equals(value);
    }

    public static Expression<Func<Image, bool>> QGroupIsNull()
    {
        return arg => arg.Group == null;
    }

    public static Expression<Func<Image, bool>> QNameEquals(string value)
    {
        return arg => arg.Name!.Equals(value);
    }

    public static Expression<Func<Image, bool>> QNameContains(string value)
    {
        return arg => arg.Name!.Contains(value);
    }
}
