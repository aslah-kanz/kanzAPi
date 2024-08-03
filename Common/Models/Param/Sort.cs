namespace KanzApi.Common.Models.Param;

public class Sort
{

    private string _name;
    public string Name { get { return _name; } set { _name = value; } }

    private EOrder _order = EOrder.Asc;
    public EOrder Order { get { return _order; } set { _order = value; } }

    public Sort(string name)
    {
        _name = name;
    }

    public Sort(string name, EOrder order)
    {
        _name = name;
        _order = order;
    }

    public static Sort From(ISortableParam param)
    {
        return new Sort(param.Sort.ToString(), param.Order);
    }
}
