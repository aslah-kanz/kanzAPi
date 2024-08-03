using System.Linq.Expressions;
using KanzApi.Common.Entities;

namespace KanzApi.Common.Models.Param;

public interface ISearchableParam<T> : ISortableParam
where T : CommonEntity
{

    string? Search { get; }

    Expression<Func<T, bool>> ToPredicate();
}

public interface ISearchableParam<E, T> : ISearchableParam<T>, ISortableParam<E>
where E : Enum
where T : CommonEntity
{ }
