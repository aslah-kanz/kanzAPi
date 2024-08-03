using KanzApi.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace KanzApi.Account.Entities;

[Table("Store", Schema = "Account")]
public class Store : CommonEntity<int?>
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }

    private string? _code;

    [MaxLength(100)]
    public string? Code { get { return _code; } set { _code = value; } }

    [MaxLength(20)]
    public string? WarehouseId { get; set; }

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

    [MaxLength(255)]
    public string? Address { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(100)]
    public string? Country { get; set; }

    [Required]
    [Column(TypeName = "decimal(12,8)")]
    public decimal? Latitude { get; set; }

    [Required]
    [Column(TypeName = "decimal(13,8)")]
    public decimal? Longitude { get; set; }

    [Required]
    public int? SaleItemCount { get; set; } = 0;

    [MaxLength(20)]
    [Column(TypeName = "nvarchar(20)")]
    public EStoreType? Type { get; set; }

    [Required]
    [MaxLength(30)]
    [Column(TypeName = "nvarchar(30)")]
    public EActivableStatus? Status { get; set; } = EActivableStatus.Active;

    public virtual ISet<Principal> Principals { get; set; } = new HashSet<Principal>();

    public string OtoStoreName { get { return "Kanzway - " + _code; } }

    public static Expression<Func<Store, bool>> QNameContains(string value)
    {
        return arg => arg.Name!.Contains(value);
    }

    public static Expression<Func<Store, bool>> QCityContains(string value)
    {
        return arg => arg.City!.Contains(value);
    }

    public static Expression<Func<Store, bool>> QCountryContains(string value)
    {
        return arg => arg.Country!.Contains(value);
    }

    public static Expression<Func<Store, bool>> QPrincipalIdEquals(int value)
    {
        return arg => arg.PrincipalId == value;
    }

    public static Expression<Func<Store, bool>> QPrincipalIdsContains(int value)
    {
        return arg => arg.Principals.Any(p => value == ((int)p.Id!));
    }

    public static Expression<Func<Store, bool>> QStatusEquals(EActivableStatus value)
    {
        return arg => arg.Status == value;
    }

    public static Expression<Func<Store, bool>> QStatusNotEquals(EActivableStatus value)
    {
        return arg => arg.Status != value;
    }
}
