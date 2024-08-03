namespace KanzApi.Common.Models.Param;

public abstract class SortableParam<E> : ISortableParam<E>
where E : Enum
{

    private E _sort;

    public E Sort { get { return _sort; } set { _sort = value; } }

    Enum ISortableParam.Sort { get { return _sort; } }

    private EOrder _order = EOrder.Desc;

    public EOrder Order { get { return _order; } set { _order = value; } }

    public SortableParam(E sort)
    {
        _sort = sort;
    }

    public SortableParam(E sort, EOrder order)
    {
        _sort = sort;
        _order = order;
    }
}
