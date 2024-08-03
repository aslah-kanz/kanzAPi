using KanzApi.Common.Models.Param;

namespace KanzApi.Product.Models.Param;

public class ProductImageSortableParam : SortableParam<EProductImageSort>
{

    public ProductImageSortableParam() : base(EProductImageSort.SortOrder, EOrder.Asc) { }
}
