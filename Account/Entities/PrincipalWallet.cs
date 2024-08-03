using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("PrincipalWallet", Schema = "Account")]
public class PrincipalWallet : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [MaxLength(100)]
    public string? Code { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

    [Required]
    [MaxLength(6)]
    [Column(TypeName = "nvarchar(6)")]
    public EWalletType? Type { get; set; }

    [MaxLength(65)]
    public string? ReferenceId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Amount { get; set; }

    public static Expression<Func<PrincipalWallet, bool>> QTypeEquals(EWalletType value)
    {
        return arg => arg.Type!.Equals(value);
    }

    public static Expression<Func<PrincipalWallet, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }
}
