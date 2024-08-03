namespace KanzApi.Common.Models.Param;

public class FaqSortableParam : SortableParam<EFaqSort>
{

    public FaqSortableParam() : base(EFaqSort.Id, EOrder.Asc) { }
}
