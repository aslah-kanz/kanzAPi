using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Common.Entities;

[Table("Document", Schema = "Common")]
[Index(nameof(Name), IsUnique = true)]
public class Document : CommonEntity<long?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Url { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Type { get; set; }

    public static Expression<Func<Document, bool>> QNameEquals(string value)
    {
        return arg => arg.Name!.Equals(value);
    }

    public static Expression<Func<Document, bool>> QNameContains(string value)
    {
        return arg => arg.Name!.Contains(value);
    }
}
