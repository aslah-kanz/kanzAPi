using KanzApi.Common.Entities;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Linq.Expressions;

namespace KanzApi.Product.Models.Param;

public class BrandPageableParam : PageableParam<EBrandSort, Brand>
{

    private string? _startsWith;

    [SwaggerParameter("<i>Starts with</i> : name")]
    public string? StartsWith { get { return _startsWith; } set { _startsWith = value; } }

    private bool? _showAtHomePage;

    public bool? ShowAtHomePage { get { return _showAtHomePage; } set { _showAtHomePage = value; } }

    private ERecordStatus? _status;

    public ERecordStatus? Status { get { return _status; } set { _status = value; } }

    private bool _hasCategories;

    [DefaultValue(false)]
    public bool HasCategories { get { return _hasCategories; } set { _hasCategories = value; } }

    private ISet<string> _categories = new HashSet<string>();

    [SwaggerParameter("<i>Category Slugs</i>")]
    public ISet<string> Categories { get { return _categories; } set { _categories = value; } }

    [SwaggerParameter("<i>Contains</i> : name")]
    public override string? Search { get; set; }

    public BrandPageableParam() : base(EBrandSort.UpdatedAt) { }

    protected override Expression<Func<Brand, bool>> ToSearchPredicate(string search)
    {
        return Brand.QNameContains(search);
    }

    public override Expression<Func<Brand, bool>> ToPredicate()
    {
        Expression<Func<Brand, bool>> result = base.ToPredicate();

        if (!String.IsNullOrEmpty(_startsWith))
        {
            result = result.And(Brand.QNameStartsWith(_startsWith));
        }

        if (_showAtHomePage != null)
        {
            result = result.And(Brand.QShowAtHomePageEquals((bool)_showAtHomePage));
        }

        if (_status != null)
        {
            result = result.And(Brand.QStatusEquals((ERecordStatus)_status));
        }

        if (_hasCategories)
        {
            result = result.And(Brand.QHasCategories());
        }

        if (_categories.Any())
        {
            result = result.And(Brand.QCategorySlugsContains(_categories));
        }

        return result;
    }
}
