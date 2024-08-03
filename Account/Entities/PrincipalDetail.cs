using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("PrincipalDetail", Schema = "Account")]
[Index(nameof(PrincipalId), IsUnique = true)]
public class PrincipalDetail : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private int? _principalId;

    [Required]
    public int? PrincipalId { get { return _principalId; } set { _principalId = value; } }

    private int? _countryId;

    [Required]
    public int? CountryId { get { return _countryId; } set { _countryId = value; } }

    private Country? _country;

    public virtual Country? Country
    { get { return _country; } set { _countryId = value?.Id; _country = value; } }

    [Required]
    [MaxLength(125)]
    public string? City { get; set; }

    [Required]
    [MaxLength(125)]
    public string? CompanyNumber { get; set; }

    [Required]
    [MaxLength(125)]
    public string? CompanyNameEn { get; set; }

    [Required]
    [MaxLength(125)]
    public string? CompanyNameAr { get; set; }

    public virtual ISet<PrincipalDetailItem> Properties { get; set; } = new HashSet<PrincipalDetailItem>();

    public virtual ISet<Principal> Principals { get; set; } = new HashSet<Principal>();

    public static Expression<Func<PrincipalDetail, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<PrincipalDetail, bool>> QCompanyNameEquals(string value)
    {
        return arg => arg.CompanyNameEn!.Equals(value);
    }

    public static Expression<Func<PrincipalDetail, bool>> QPrincipalIdsContains(int value)
    {
        return arg => arg.Principals.Any(p => value == ((int)p.Id!));
    }
}
