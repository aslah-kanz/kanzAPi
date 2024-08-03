using System.Linq.Expressions;
using KanzApi.Account.Entities;
using KanzApi.Common.Models.Param;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Account.Models.Param;

public class StorePageableParam : PageableParam<EStoreSort, Store>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public StorePageableParam() : base(EStoreSort.UpdatedAt) { }

    protected override Expression<Func<Store, bool>> ToSearchPredicate(string search)
    {
        return Store.QNameContains(search);
    }
}
