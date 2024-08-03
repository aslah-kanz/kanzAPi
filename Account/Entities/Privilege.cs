using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("Privilege", Schema = "Account")]
[Index(nameof(Name), IsUnique = true)]
public class Privilege : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public virtual ISet<Role> Roles { get; set; } = new HashSet<Role>();

    public static Expression<Func<Privilege, bool>> QNameContains(string value)
    {
        return arg => arg.Name!.Contains(value);
    }
}
