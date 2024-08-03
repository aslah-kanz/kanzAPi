using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Account.Entities;

[Table("PrincipalRole", Schema = "Account")]
public class PrincipalRole
{

    public int? PrincipalId { get; set; }

    public int? RoleId { get; set; }
}
