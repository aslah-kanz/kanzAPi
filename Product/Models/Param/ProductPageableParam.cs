using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using ProductEntity = KanzApi.Product.Entities.Product;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Product.Models.Param;

public class ProductPageableParam : PageableParam<EProductSort, ProductEntity>
{

    private int? _brandId;

    public int? BrandId { get { return _brandId; } set { _brandId = value; } }

    private ISet<string> _categories = new HashSet<string>();

    [SwaggerParameter("<i>Category Slugs</i>")]
    public ISet<string> Categories { get { return _categories; } set { _categories = value; } }

    private string? _mpn;

    [SwaggerParameter("<i>Starts With</i> : mpn")]
    public string? Mpn { get { return _mpn; } set { _mpn = value; } }

    private string? _code;

    [SwaggerParameter("<i>Starts With</i> : code")]
    public string? Code { get { return _code; } set { _code = value; } }

    private string? _name;

    [SwaggerParameter("<i>Contains</i> : name")]
    public string? Name { get { return _name; } set { _name = value; } }

    public ProductPageableParam() : base(EProductSort.UpdatedAt) { }

    public override Expression<Func<ProductEntity, bool>> ToPredicate()
    {
        Expression<Func<ProductEntity, bool>> result = base.ToPredicate();

        if (_brandId != null)
        {
            result = result.And(ProductEntity.QBrandIdEquals((int)_brandId));
        }
        if (_categories.Any())
        {
            result = result.And(ProductEntity.QCategorySlugsContains(_categories));
        }
        if (_mpn != null)
        {
            result = result.And(ProductEntity.QMpnStartsWith(_mpn));
        }
        if (_code != null)
        {
            result = result.And(ProductEntity.QCodeStartsWith(_code));
        }
        if (_name != null)
        {
            result = result.And(ProductEntity.QNameContains(_name));
        }

        return result;
    }
}
