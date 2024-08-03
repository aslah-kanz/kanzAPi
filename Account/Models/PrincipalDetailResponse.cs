
namespace KanzApi.Account.Models;

public class PrincipalDetailResponse
{

    public int Id { get; set; }

    public int CountryId { get; set; }

    public string? City { get; set; }

    public string? CompanyNumber { get; set; }

    public string? CompanyNameEn { get; set; }

    public string? CompanyNameAr { get; set; }

    public ISet<PrincipalDetailItemRequest>? Properties { get; set; }
}
