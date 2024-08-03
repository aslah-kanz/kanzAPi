using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Transaction.Entities;

[Table("Withdraw", Schema = "Transaction")]
public class Withdraw : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    { get { return _principal; } set { _principalId = value?.Id; _principal = value; } }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Amount { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    [Required]
    [MaxLength(50)]
    public string WithdrawMethod { get; set; }

    [Required]
    [MaxLength(100)]
    public string BankName { get; set; }

    [Required]
    [MaxLength(50)]
    public string BankHolder { get; set; }

    [Required]
    [MaxLength(50)]
    public string AccountNumber { get; set; }

    [Required]
    [MaxLength(10)]
    [Column(TypeName = "nvarchar(10)")]
    public EWithdrawStatus? Status { get; set; } = EWithdrawStatus.Pending;

    public static Expression<Func<Withdraw, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }
}
