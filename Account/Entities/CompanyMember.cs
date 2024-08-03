using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Account.Entities;

[Table("CompanyMember", Schema = "Account")]
public class CompanyMember
{

    public int? PrincipalId { get; set; }

    public int? PrincipalDetailId { get; set; }
}
