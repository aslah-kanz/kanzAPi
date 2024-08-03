using KanzApi.Common.Models.Param;
using Microsoft.EntityFrameworkCore;

namespace KanzApi.Extensions;

public static class IQueryables
{

    public static IQueryable<T> Sort<T>(this IQueryable<T> query, Sort sort)
    {
        return sort.Order == EOrder.Asc
            ? query.OrderBy(e => EF.Property<T>(e!, sort.Name))
            : query.OrderByDescending(e => EF.Property<T>(e!, sort.Name));
    }
}
