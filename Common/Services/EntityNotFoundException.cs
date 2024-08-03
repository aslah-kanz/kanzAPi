using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Common.Services;

public class EntityNotFoundException(Type type, object value) : CommonException(ErrorCode.EntityNotFound, type.Name, value)
{

    public static EntityNotFoundException From(Type type, IDictionary<string, object?> pairs)
    {
        string value = String.Join(", ", pairs.Select(kv => kv.Key + " = " + kv.Value));
        return new EntityNotFoundException(type, value);
    }

    public static EntityNotFoundException From(Type type, string key, object? value)
    {
        return From(type, new Dictionary<string, object?> { { key, value } });
    }

    public static EntityNotFoundException From(Type type, object? id)
    {
        return From(type, "ID", id);
    }
}
