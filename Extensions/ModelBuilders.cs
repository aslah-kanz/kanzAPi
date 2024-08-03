using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KanzApi.Extensions;

public static class ModelBuilders
{

    public static void BindManyToMany<T, U, V>(this ModelBuilder builder,
    Expression<Func<T, IEnumerable<U>?>> expression)
    where T : class where U : class where V : class
    {
        builder.Entity<T>()
        .HasMany(expression).WithMany()
        .UsingEntity<V>();
    }

    public static void BindManyToMany<T, U, V>(this ModelBuilder builder,
    Expression<Func<T, IEnumerable<U>?>> expression1, Expression<Func<U, IEnumerable<T>?>> expression2)
    where T : class where U : class where V : class
    {
        builder.Entity<T>()
        .HasMany(expression1).WithMany(expression2)
        .UsingEntity<V>();
    }
}
