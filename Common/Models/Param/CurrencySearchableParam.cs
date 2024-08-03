using KanzApi.Common.Entities;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Common.Models.Param;

public class CurrencySearchableParam : SearchableParam<ECurrencySort, Currency>
{

    [SwaggerParameter("<i>Contains</i> : code, country, description")]
    public override string? Search { get; set; }

    public CurrencySearchableParam() : base(ECurrencySort.Id, EOrder.Asc) { }

    protected override Expression<Func<Currency, bool>> ToSearchPredicate(string search)
    {
        return Currency.QCodeEquals(search).Or(
            Currency.QCountryEquals(search),
            Currency.QDescriptionContains(search));
    }
}
