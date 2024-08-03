namespace KanzApi.Common.Models.Param;

public interface ISortableParam
{

    Enum Sort { get; }

    public EOrder Order { get; }
}

public interface ISortableParam<E> : ISortableParam
where E : Enum
{

    new E Sort { get; }
}
