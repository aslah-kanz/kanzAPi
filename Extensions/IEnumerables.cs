
using System.Collections;

namespace KanzApi.Extensions;

public static class IEnumerables
{

    public static int Count(this IEnumerable source)
    {
        if (source is ICollection c) return c.Count;

        int result = 0;
        IEnumerator enumerator = source.GetEnumerator();
        while (enumerator.MoveNext()) result++;

        return result;
    }

    public static T? Find<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        foreach (T o in source)
        {
            if (predicate(o))
            {
                return o;
            }
        }
        return default;
    }
}
