using KanzApi.Common.Entities;

namespace KanzApi.Common.Models.Param;

public interface IPageableParam<T> : ISearchableParam<T>
where T : CommonEntity
{

    int Page { get; }

    int Size { get; }
}

public interface IPageableParam<E, T> : IPageableParam<T>, ISearchableParam<E, T>
where E : Enum
where T : CommonEntity
{ }
