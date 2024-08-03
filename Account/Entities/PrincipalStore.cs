using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Account.Entities;

[Table("PrincipalStore", Schema = "Account")]
public class PrincipalStore
{

    public int? PrincipalId { get; set; }

    public int? StoreId { get; set; }
}
