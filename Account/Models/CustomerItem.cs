using KanzApi.Account.Entities;
using KanzApi.Common.Models;

namespace KanzApi.Account.Models;

public class CustomerItem
{

    public int Id { get; set; }

    public EPrincipalType? Type { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? Email { get; set; } = "";

    public string? CountryCode { get; set; }

    public string? PhoneNumber { get; set; }

    public string Gender { get; set; } = "";

    public DateOnly? BirthDate { get; set; }

    public ImageResponse? Image { get; set; }

    public ISet<RoleItem> Roles { get; set; } = new HashSet<RoleItem>();

    public ISet<DepartmentResponse> Departments { get; set; } = new HashSet<DepartmentResponse>();
}
