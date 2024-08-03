using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("PrincipalAddress", Schema = "Account")]
[Index(nameof(PrincipalId), nameof(Name), IsUnique = true)]
public class PrincipalAddress : CommonEntity<int?>
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
    [MaxLength(255)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string? RecipientName { get; set; }

    private string? _countryCode;

    [MaxLength(4)]
    public string? CountryCode { get { return _countryCode; } set { _countryCode = value; } }

    private string? _phoneNumber;

    [MaxLength(15)]
    public string? PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

    [Required]
    [MaxLength(255)]
    public string? Address { get; set; }

    [Required]
    [MaxLength(125)]
    public string? City { get; set; }

    [Required]
    [MaxLength(125)]
    public string? Country { get; set; }

    [Required]
    [Column(TypeName = "decimal(12,8)")]
    public decimal? Latitude { get; set; }

    [Required]
    [Column(TypeName = "decimal(13,8)")]
    public decimal? Longitude { get; set; }

    [Required]
    public bool? DefaultAddress { get; set; } = false;

    public static Expression<Func<PrincipalAddress, bool>> QIdNotEquals(int value)
    {
        return arg => arg.Id != value;
    }

    public static Expression<Func<PrincipalAddress, bool>> QNameEquals(string value)
    {
        return arg => arg.Name!.Equals(value);
    }

    public static Expression<Func<PrincipalAddress, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<PrincipalAddress, bool>> QDefaultAddressEquals(bool value)
    {
        return arg => arg.DefaultAddress == value;
    }
}
