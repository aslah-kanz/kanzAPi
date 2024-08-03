using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Common.Models;

namespace KanzApi.Common.Entities;

[Table("JobField", Schema = "Common")]
public class JobField : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    public LocalizableString? Name { get; set; }

    public static Expression<Func<JobField, bool>> QNameContains(string value)
    {
        return arg
        => (arg.Name!.En != null && arg.Name.En.Contains(value))
        || (arg.Name.Ar != null && arg.Name.Ar.Contains(value));
    }
}
