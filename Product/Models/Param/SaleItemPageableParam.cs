using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using KanzApi.Product.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq.Expressions;

namespace KanzApi.Product.Models.Param;

public class SaleItemPageableParam : PageableParam<ESaleItemSort, SaleItem>
{

    private bool? _enabled;

    public bool? Enabled { get { return _enabled; } set { _enabled = value; } }

    private ISet<string> _categories = new HashSet<string>();

    [SwaggerParameter("<i>Category Slugs</i>")]
    public ISet<string> Categories { get { return _categories; } set { _categories = value; } }

    private int? _storeId;

    public int? StoreId { get { return _storeId; } set { _storeId = value; } }

    private int? _brandId;

    public int? BrandId { get { return _brandId; } set { _brandId = value; } }

    private int? _minStock;

    public int? MinStock { get { return _minStock; } set { _minStock = value; } }

    private int? _maxStock;

    public int? MaxStock { get { return _maxStock; } set { _maxStock = value; } }

    private int? _productId;

    public int? ProductId { get { return _productId; } set { _productId = value; } }

    private string? _mpn;

    [SwaggerParameter("<i>Starts With</i> : mpn")]
    public string? Mpn { get { return _mpn; } set { _mpn = value; } }

    private string? _vendorSku;

    [SwaggerParameter("<i>Contains</i> : vendorSku")]
    public string? VendorSku { get { return _vendorSku; } set { _vendorSku = value; } }

    public SaleItemPageableParam() : base(ESaleItemSort.UpdatedAt) { }

    public override Expression<Func<SaleItem, bool>> ToPredicate()
    {
        Expression<Func<SaleItem, bool>> result = base.ToPredicate();

        if (_enabled != null)
        {
            result = result.And(SaleItem.QEnabledEquals((bool)_enabled));
        }
        if (_storeId != null)
        {
            result = result.And(SaleItem.QStoreIdEquals((int)_storeId));
        }
        if (_brandId != null)
        {
            result = result.And(SaleItem.QBrandIdEquals((int)_brandId));
        }
        if (_categories.Any())
        {
            result = result.And(SaleItem.QCategorySlugsContains(_categories));
        }
        if (_productId != null)
        {
            result = result.And(SaleItem.QProductIdEquals((int)_productId));
        }
        if (_mpn != null)
        {
            result = result.And(SaleItem.QMpnStartsWith(_mpn));
        }
        if (_vendorSku != null)
        {
            result = result.And(SaleItem.QVendorSkuContains(_vendorSku));
        }
        if (_minStock != null && _maxStock != null)
        {
            result = result.And(SaleItem.QStockBetween((int)_minStock, (int)_maxStock));
        }
        else if (_minStock != null)
        {
            result = result.And(SaleItem.QStockGreaterThanOrEquals((int)_minStock));
        }
        else if (_maxStock != null)
        {
            result = result.And(SaleItem.QStockLessThanOrEquals((int)_maxStock));
        }

        return result;
    }
}
