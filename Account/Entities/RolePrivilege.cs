using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Account.Entities;

[Table("RolePrivilege", Schema = "Account")]
public class RolePrivilege
{

    public int? RoleId { get; set; }

    public int? PrivilegeId { get; set; }
}
