using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Common.Entities;

[Table("Subscriber", Schema = "Common")]
public class Subscriber : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

    public static Expression<Func<Subscriber, bool>> QEmailContains(string value)
    {
        return arg => arg.Email != null && arg.Email.Contains(value);
    }
}
