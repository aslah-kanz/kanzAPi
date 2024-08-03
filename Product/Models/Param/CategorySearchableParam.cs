using KanzApi.Common.Entities;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Product.Models.Param;

public class CategorySearchableParam : SearchableParam<ECategorySort, Category>
{

    private ERecordStatus? _status;

    public ERecordStatus? Status { get { return _status; } set { _status = value; } }

    private ISet<int> _brands = new HashSet<int>();

    [SwaggerParameter("<i>Brand IDs</i>")]
    public ISet<int> Brands { get { return _brands; } set { _brands = value; } }

    [SwaggerParameter("<i>Contains</i> : name, description")]
    public override string? Search { get; set; }

    public CategorySearchableParam() : base(ECategorySort.UpdatedAt) { }

    protected override Expression<Func<Category, bool>> ToSearchPredicate(string search)
    {
        return Category.QNameContains(search).Or(
            Category.QDescriptionContains(search));
    }

    public override Expression<Func<Category, bool>> ToPredicate()
    {
        Expression<Func<Category, bool>> result = base.ToPredicate();

        if (_status != null)
        {
            result = result.And(Category.QStatusEquals((ERecordStatus)_status));
        }

        if (_brands.Any())
        {
            result = result.And(Category.QBrandIdsContains(_brands));
        }

        return result;
    }
}
