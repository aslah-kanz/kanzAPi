using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace KanzApi.Common.Entities;

public abstract class CommonEntity : IIdentifiable, IAuditable
{

    public object? Id => throw new NotImplementedException();

    [Required]
    public DateTime? CreatedAt { get; set; }

    [Required]
    public int? CreatedBy { get; set; }

    [Required]
    public DateTime? UpdatedAt { get; set; }

    [Required]
    public int? UpdatedBy { get; set; }

    public static Expression<Func<T, bool>> QIdEquals<T>(object value) where T : CommonEntity
    {
        return arg => value.Equals(arg.Id);
    }

    public static Expression<Func<T, bool>> QCreatedAtLessThanOrEquals<T>(DateTime value) where T : CommonEntity
    {
        return arg => arg.CreatedAt <= value;
    }

    public static Expression<Func<T, bool>> QCreatedAtLessThanOrEquals<T>(DateOnly value) where T : CommonEntity
    {
        return QCreatedAtLessThanOrEquals<T>(value.ToDateTime(TimeOnly.MaxValue));
    }

    public static Expression<Func<T, bool>> QCreatedAtGreaterThanOrEquals<T>(DateTime value) where T : CommonEntity
    {
        return arg => arg.CreatedAt >= value;
    }

    public static Expression<Func<T, bool>> QCreatedAtGreaterThanOrEquals<T>(DateOnly value) where T : CommonEntity
    {
        return QCreatedAtGreaterThanOrEquals<T>(value.ToDateTime(TimeOnly.MinValue));
    }

    public static Expression<Func<T, bool>> QUpdatedAtLessThanOrEquals<T>(DateTime value) where T : CommonEntity
    {
        return arg => arg.UpdatedAt <= value;
    }

    public static Expression<Func<T, bool>> QUpdatedAtLessThanOrEquals<T>(DateOnly value) where T : CommonEntity
    {
        return QUpdatedAtLessThanOrEquals<T>(value.ToDateTime(TimeOnly.MaxValue));
    }

    public static Expression<Func<T, bool>> QUpdatedAtGreaterThanOrEquals<T>(DateTime value) where T : CommonEntity
    {
        return arg => arg.UpdatedAt >= value;
    }

    public static Expression<Func<T, bool>> QUpdatedAtGreaterThanOrEquals<T>(DateOnly value) where T : CommonEntity
    {
        return QUpdatedAtGreaterThanOrEquals<T>(value.ToDateTime(TimeOnly.MinValue));
    }
}

public abstract class CommonEntity<ID> : CommonEntity, IIdentifiable<ID>
{

    public new abstract ID Id { get; set; }
}
