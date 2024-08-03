using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanzApi.Account.Entities;

[Table("PrincipalDetailItem", Schema = "Account")]
public class PrincipalDetailItem : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _principalDetailId;

    [Required]
    public int? PrincipalDetailId { get { return _principalDetailId; } set { _principalDetailId = value; } }

    private PrincipalDetail? _principalDetail;

    public virtual PrincipalDetail? PrincipalDetail
    { get { return _principalDetail; } set { _principalDetailId = value?.Id; _principalDetail = value; } }

    [Required]
    public string? Description { get; set; }

    [Required]
    public string? Value { get; set; }
}
