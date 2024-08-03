using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Account.Entities;

[Table("PrincipalDepartment", Schema = "Account")]
public class PrincipalDepartment
{

    public int? PrincipalId { get; set; }

    public int? DepartmentId { get; set; }
}
