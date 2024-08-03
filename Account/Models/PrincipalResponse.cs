using KanzApi.Account.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Account.Models;

public class PrincipalResponse
{

    public int Id { get; set; }

    public string Username { get; set; } = "";

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? Email { get; set; } = "";

    public EGender? Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public ImageResponse? Image { get; set; }

    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }

    public ISet<RoleResponse?> Roles { get; set; } = new HashSet<RoleResponse?>();

    public ISet<DepartmentResponse?> Departments { get; set; } = new HashSet<DepartmentResponse?>();

    public ISet<StoreResponse?> Stores { get; set; } = new HashSet<StoreResponse?>();

    public ISet<PrincipalDetailPrincipalResponse?> PrincipalDetails { get; set; } = new HashSet<PrincipalDetailPrincipalResponse?>();

    public EPrincipalType? Type { get; set; }

    public EPrincipalStatus? Status { get; set; }
}
