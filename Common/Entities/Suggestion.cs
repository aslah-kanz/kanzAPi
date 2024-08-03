using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Common.Entities;

[Table("Suggestion", Schema = "Common")]
public class Suggestion : CommonEntity<int?>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(65)]
    public string? PhoneNumber { get; set; }

    [MaxLength(255)]
    public string? Subject { get; set; }

    [MaxLength(255)]
    public string? Message { get; set; }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public ESuggestionStatus? Status { get; set; } = ESuggestionStatus.Unread;

    public static Expression<Func<Suggestion, bool>> QSubjectContains(string value)
    {
        return arg => arg.Subject != null && arg.Subject.Contains(value);
    }
}
