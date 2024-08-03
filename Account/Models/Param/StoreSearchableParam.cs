using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class StoreSearchableParam : SearchableParam<EStoreSort, Store>
{

    [SwaggerParameter("<i>Contains</i> : name, city, country")]
    public override string? Search { get; set; }

    public StoreSearchableParam() : base(EStoreSort.UpdatedAt) { }

    protected override Expression<Func<Store, bool>> ToSearchPredicate(string search)
    {
        return Store.QNameContains(search).Or(Store.QCityContains(search), Store.QCountryContains(search));
    }
}
