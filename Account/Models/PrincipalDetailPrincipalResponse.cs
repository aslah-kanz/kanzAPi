
using KanzApi.Common.Models;

namespace KanzApi.Account.Models;

public class PrincipalDetailPrincipalResponse
{

    public int Id { get; set; }

    public CountryResponse? Country { get; set; }

    public string? City { get; set; }

    public string? CompanyNumber { get; set; }

    public string? CompanyNameEn { get; set; }

    public string? CompanyNameAr { get; set; }
}
