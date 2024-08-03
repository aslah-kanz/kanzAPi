using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using System.Text.Json;

namespace KanzApi.Extensions;

public static class EntityTypeBuilders
{

    public static void BindOneToMany<T, U>(this EntityTypeBuilder<T> builder, Expression<Func<T, U?>> expression)
    where T : class where U : class
    {
        builder
        .HasOne(expression).WithMany()
        .OnDelete(DeleteBehavior.Restrict);
    }

    public static EntityTypeBuilder<T> BindJson<T, U>(this EntityTypeBuilder<T> builder, Expression<Func<T, U?>> expression)
    where T : class where U : class
    {
        return builder.OwnsOne(expression, a => a.ToJson());
    }

    public static EntityTypeBuilder<T> BindJsons<T, U>(this EntityTypeBuilder<T> builder, Expression<Func<T, IEnumerable<U>?>> expression)
    where T : class where U : class
    {
        return builder.OwnsMany(expression, a => a.ToJson());
    }

    public static EntityTypeBuilder<T> BindJsonArray<T, U>(this EntityTypeBuilder<T> builder, Expression<Func<T, List<U>?>> expression)
    where T : class where U : class
    {
        builder.Property(expression)
        .HasConversion(
            v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
            v => JsonSerializer.Deserialize<List<U>>(v, default(JsonSerializerOptions)),
            new ValueComparer<List<U>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

        return builder;
    }

    public static EntityTypeBuilder<T> BindJsonObject<T, U, V>(this EntityTypeBuilder<T> builder, Expression<Func<T, Dictionary<U, V>?>> expression)
    where T : class where U : class
    {
        builder.Property(expression)
        .HasConversion(
            v => JsonSerializer.Serialize(v, default(JsonSerializerOptions)),
            v => JsonSerializer.Deserialize<Dictionary<U, V>>(v, default(JsonSerializerOptions)),
            new ValueComparer<Dictionary<U, V>>(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToDictionary()));

        return builder;
    }
}
