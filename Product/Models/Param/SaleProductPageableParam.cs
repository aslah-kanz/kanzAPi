using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using ProductEntity = KanzApi.Product.Entities.Product;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Product.Models.Param;

public class SaleProductPageableParam : PageableParam<ESaleProductSort, ProductEntity>
{

    private decimal? _lowestPrice;

    public decimal? LowestPrice { get { return _lowestPrice; } set { _lowestPrice = value; } }

    private decimal? _highestPrice;

    public decimal? HighestPrice { get { return _highestPrice; } set { _highestPrice = value; } }

    [SwaggerParameter("<i>Starts With</i> : code, mpn")]
    public override string? Search { get; set; }

    public SaleProductPageableParam() : base(ESaleProductSort.Mpn, EOrder.Asc) { }

    protected override Expression<Func<ProductEntity, bool>> ToSearchPredicate(string search)
    {
        return ProductEntity.QFamilyCodeStartsWith(search).Or(
            ProductEntity.QMpnStartsWith(search));
    }

    public override Expression<Func<ProductEntity, bool>> ToPredicate()
    {
        Expression<Func<ProductEntity, bool>> result = base.ToPredicate();

        if (_lowestPrice != null && _highestPrice != null)
        {
            result = result.And(ProductEntity.QPriceBetween((decimal)_lowestPrice, (decimal)_highestPrice));
        }
        else if (_lowestPrice != null)
        {
            result = result.And(ProductEntity.QPriceGreaterThanOrEquals((decimal)_lowestPrice));
        }
        else if (_highestPrice != null)
        {
            result = result.And(ProductEntity.QPriceLessThanOrEquals((decimal)_highestPrice));
        }

        return result;
    }
}
