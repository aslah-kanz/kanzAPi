namespace KanzApi.Extensions;

public static class Objects
{

    public static Dictionary<string, object?> ToJsonDictionary(this object source)
    {
        return source.GetType().GetProperties()
        .ToDictionary(property => property.Name.ToLowerFirstChar(), property => property.GetValue(source));
    }
}
