using KanzApi.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("Principal", Schema = "Account")]
[Index(nameof(Username), IsUnique = true)]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(CountryCode), nameof(PhoneNumber), IsUnique = true)]
public class Principal : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    private long? _imageId;

    public long? ImageId { get { return _imageId; } set { _imageId = value; } }

    private Image? _image;

    public virtual Image? Image
    { get { return _image; } set { _imageId = value?.Id; _image = value; } }

    [Required]
    [MaxLength(100)]
    public string? Username { get; set; }

    [MaxLength(255)]
    public string? Password { get; set; }

    private string? _firstName;

    [Required]
    [MaxLength(100)]
    public string? FirstName { get { return _firstName; } set { _firstName = value; } }

    private string? _lastName;

    [Required]
    [MaxLength(100)]
    public string? LastName { get { return _lastName; } set { _lastName = value; } }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

    private string? _countryCode;

    [MaxLength(4)]
    public string? CountryCode { get { return _countryCode; } set { _countryCode = value; } }

    private string? _phoneNumber;

    [MaxLength(15)]
    public string? PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }

    private EPrincipalType? _type;

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EPrincipalType? Type { get { return _type; } set { _type = value; } }

    [MaxLength(6)]
    [Column(TypeName = "nvarchar(6)")]
    public EGender? Gender { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? BirthDate { get; set; }

    [Required]
    public bool? AcceptNewsLetter { get; set; } = true;

    [MaxLength(255)]
    public string? Comment { get; set; }

    [Required]
    [MaxLength(30)]
    [Column(TypeName = "nvarchar(30)")]
    public EPrincipalStatus? Status { get; set; } = EPrincipalStatus.Inactive;

    public string? WebConfig
    {
        get
        {
            return _type switch
            {
                EPrincipalType.Admin => "AdminClient",
                EPrincipalType.Vendor or EPrincipalType.Manufacture => "VendorClient",
                _ => "WebClient",
            };
        }
    }

    public virtual ISet<Role> Roles { get; set; } = new HashSet<Role>();

    public virtual ISet<Store> Stores { get; set; } = new HashSet<Store>();

    public virtual ISet<Department> Departments { get; set; } = new HashSet<Department>();

    public virtual ISet<PrincipalDetail> PrincipalDetails { get; set; } = new HashSet<PrincipalDetail>();

    public string FullName { get { return _firstName + " " + _lastName; } }

    public string FullPhoneNumber { get { return _countryCode + _phoneNumber; } }

    public static Expression<Func<Principal, bool>> QUsernameEquals(string value)
    {
        return arg => arg.Username!.Equals(value);
    }

    public static Expression<Func<Principal, bool>> QUsernameContains(string value)
    {
        return arg => arg.Username!.Contains(value);
    }

    public static Expression<Func<Principal, bool>> QFirstNameContains(string value)
    {
        return arg => arg.FirstName!.Contains(value);
    }

    public static Expression<Func<Principal, bool>> QLastNameContains(string value)
    {
        return arg => arg.LastName!.Contains(value);
    }

    public static Expression<Func<Principal, bool>> QStatusEquals(EPrincipalStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<Principal, bool>> QStatusNotEquals(EPrincipalStatus value)
    {
        return arg => arg.Status != value;
    }

    public static Expression<Func<Principal, bool>> QPrincipalDetailIdsContains(int value)
    {
        return arg => arg.PrincipalDetails.Any(p => p.Id == value);
    }

    public static Expression<Func<Principal, bool>> QEmailEquals(string value)
    {
        return arg => arg.Email!.Equals(value);
    }

    public static Expression<Func<Principal, bool>> QEmailContains(string value)
    {
        return arg => arg.Email!.Contains(value);
    }

    public static Expression<Func<Principal, bool>> QTypeEquals(EPrincipalType value)
    {
        return arg => arg.Type == value;
    }

    public static Expression<Func<Principal, bool>> QGenderEquals(EGender value)
    {
        return arg => arg.Gender == value;
    }

    public static Expression<Func<Principal, bool>> QPhoneNumberStartsWith(string value)
    {
        return arg => arg.PhoneNumber!.StartsWith(value);
    }
}
