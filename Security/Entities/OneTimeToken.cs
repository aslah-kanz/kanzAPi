using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Security.Entities;

[Table("OneTimeToken", Schema = "Security")]
[Index(nameof(Token), IsUnique = true)]
[Index(nameof(Type), nameof(PrincipalId), IsUnique = true)]
public class OneTimeToken : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [Required]
    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EOneTimeTokenType? Type { get; set; }

    [Required]
    [MaxLength(255)]
    public string? Token { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private Principal? _principal;

    public virtual Principal? Principal
    {
        get { return _principal; }
        set
        {
            if (value != null) _principalId = value.Id;
            _principal = value;
        }
    }

    private DateTime? _expiredAt;

    [Required]
    public DateTime? ExpiredAt { get { return _expiredAt; } set { _expiredAt = value; } }

    public bool IsExpired() { return DateTime.Now >= _expiredAt; }

    public static Expression<Func<OneTimeToken, bool>> QTypeEquals(EOneTimeTokenType value)
    {
        return arg => arg.Type == value;
    }

    public static Expression<Func<OneTimeToken, bool>> QTokenEquals(string value)
    {
        return arg => value.Equals(arg.Token);
    }

    public static Expression<Func<OneTimeToken, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }
}
