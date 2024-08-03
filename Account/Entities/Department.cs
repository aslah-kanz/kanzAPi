using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("Department", Schema = "Account")]
public class Department : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _principalDetailId;

    [Required]
    public int? PrincipalDetailId { get { return _principalDetailId; } set { _principalDetailId = value; } }

    private PrincipalDetail? _principalDetail;

    public virtual PrincipalDetail? PrincipalDetail
    { get { return _principalDetail; } set { _principalDetailId = value?.Id; _principalDetail = value; } }

    [MaxLength(20)]
    public string? Code { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Name { get; set; }

    public virtual ISet<Principal> Principals { get; set; } = new HashSet<Principal>();

    public static Expression<Func<Department, bool>> QNameEquals(string value)
    {
        return arg => arg.Name!.Equals(value);
    }

    public static Expression<Func<Department, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalDetail!.PrincipalId == value;
    }
}
