using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class CountrySearchableParam : SearchableParam<ECountrySort, Country>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public CountrySearchableParam() : base(ECountrySort.PhoneCode, EOrder.Asc) { }

    protected override Expression<Func<Country, bool>> ToSearchPredicate(string search)
    {
        return Country.QNameContains(search);
    }
}
