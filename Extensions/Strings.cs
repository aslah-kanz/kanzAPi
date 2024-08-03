using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace KanzApi.Extensions;

public static class Strings
{

    public static string ToLowerFirstChar(this string source)
    {
        return !String.IsNullOrEmpty(source) ? Char.ToLower(source[0]) + source[1..] : source;
    }

    public static string ToUpperFirstChar(this string source)
    {
        return !String.IsNullOrEmpty(source) ? Char.ToUpper(source[0]) + source[1..] : source;
    }

    public static string Format(this string source, Dictionary<string, object> values)
    {
        StringBuilder builder = new(source);
        foreach (KeyValuePair<string, object> pair in values)
        {
            builder.Replace("{" + pair.Key + "}", pair.Value.ToString());
        }
        return builder.ToString();
    }

    public static string Format(this string source, object values)
    {
        StringBuilder builder = new(source);
        foreach (PropertyInfo property in values.GetType().GetProperties())
        {
            builder.Replace(
                "{" + property.Name + "}",
                property.GetValue(values)!.ToString());
        }
        return builder.ToString();
    }

    public static string Sha256(this string source)
    {
        byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(source));
        return Convert.ToHexString(hash).ToLower();
    }
}
