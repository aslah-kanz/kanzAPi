using System.Linq.Expressions;
using KanzApi.Common.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace KanzApi.Common.Models.Param;

public class LanguageSearchableParam : SearchableParam<ELanguageSort, Language>
{

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public LanguageSearchableParam() : base(ELanguageSort.Code, EOrder.Asc) { }

    protected override Expression<Func<Language, bool>> ToSearchPredicate(string search)
    {
        return Language.QNameContains(search);
    }
}
