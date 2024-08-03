using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("Role", Schema = "Account")]
[Index(nameof(Name), IsUnique = true)]
public class Role : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    public virtual ISet<Privilege> Privileges { get; set; } = new HashSet<Privilege>();

    public virtual ISet<Principal> Principals { get; set; } = new HashSet<Principal>();

    public static Expression<Func<Role, bool>> QNameContains(string value)
    {
        return arg => arg.Name!.Contains(value);
    }
}
