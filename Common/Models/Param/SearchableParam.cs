using KanzApi.Common.Entities;
using KanzApi.Extensions;
using System.Linq.Expressions;

namespace KanzApi.Common.Models.Param;

public abstract class SearchableParam<E, T> : SortableParam<E>, ISearchableParam<E, T>
where E : Enum
where T : CommonEntity
{

    private DateOnly? _startCreatedDate;

    public DateOnly? StartCreatedDate { get { return _startCreatedDate; } set { _startCreatedDate = value; } }

    private DateTime? _startCreatedAt;

    public DateTime? StartCreatedAt { get { return _startCreatedAt; } set { _startCreatedAt = value; } }

    private DateOnly? _endCreatedDate;

    public DateOnly? EndCreatedDate { get { return _endCreatedDate; } set { _endCreatedDate = value; } }

    private DateTime? _endCreatedAt;

    public DateTime? EndCreatedAt { get { return _endCreatedAt; } set { _endCreatedAt = value; } }

    private DateOnly? _startUpdatedDate;

    public DateOnly? StartUpdatedDate { get { return _startUpdatedDate; } set { _startUpdatedDate = value; } }

    private DateTime? _startUpdatedAt;

    public DateTime? StartUpdatedAt { get { return _startUpdatedAt; } set { _startUpdatedAt = value; } }

    private DateOnly? _endUpdatedDate;

    public DateOnly? EndUpdatedDate { get { return _endUpdatedDate; } set { _endUpdatedDate = value; } }

    private DateTime? _endUpdatedAt;

    public DateTime? EndUpdatedAt { get { return _endUpdatedAt; } set { _endUpdatedAt = value; } }

    private string? _search;

    public virtual string? Search { get { return _search; } set { _search = value; } }

    public SearchableParam(E sort) : base(sort) { }

    public SearchableParam(E sort, EOrder order) : base(sort, order) { }

    protected virtual Expression<Func<T, bool>> ToSearchPredicate(string search)
    {
        return Expressions.True<T>();
    }

    public virtual Expression<Func<T, bool>> ToPredicate()
    {
        Expression<Func<T, bool>>? result = null;

        if (_startCreatedDate != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateOnly)_startCreatedDate))
            : CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateOnly)_startCreatedDate);
        }

        if (_startCreatedAt != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateTime)_startCreatedAt))
            : CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateTime)_startCreatedAt);
        }

        if (_endCreatedDate != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtLessThanOrEquals<T>((DateOnly)_endCreatedDate))
            : CommonEntity.QCreatedAtLessThanOrEquals<T>((DateOnly)_endCreatedDate);
        }

        if (_endCreatedAt != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtLessThanOrEquals<T>((DateTime)_endCreatedAt))
            : CommonEntity.QCreatedAtLessThanOrEquals<T>((DateTime)_endCreatedAt);
        }

        if (_startUpdatedDate != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateOnly)_startUpdatedDate))
            : CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateOnly)_startUpdatedDate);
        }

        if (_startUpdatedAt != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateTime)_startUpdatedAt))
            : CommonEntity.QCreatedAtGreaterThanOrEquals<T>((DateTime)_startUpdatedAt);
        }

        if (_endUpdatedDate != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtLessThanOrEquals<T>((DateOnly)_endUpdatedDate))
            : CommonEntity.QCreatedAtLessThanOrEquals<T>((DateOnly)_endUpdatedDate);
        }

        if (_endUpdatedAt != null)
        {
            result = result != null
            ? result.And(CommonEntity.QCreatedAtLessThanOrEquals<T>((DateTime)_endUpdatedAt))
            : CommonEntity.QCreatedAtLessThanOrEquals<T>((DateTime)_endUpdatedAt);
        }

        if (!String.IsNullOrEmpty(Search))
        {
            result = result != null
            ? result.And(ToSearchPredicate(Search))
            : ToSearchPredicate(Search);
        }

        return result ?? Expressions.True<T>();
    }
}
