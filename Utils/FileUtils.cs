namespace KanzApi.Utils;

public class FileUtils
{

    public static string ToPath(string? group, string name)
    {
        return group != null
            ? Path.Combine(group!.ToLower(), name!)
            : name!;
    }

    public static string ToUrl(string baseUrl, string container, string? group, string name)
    {
        return Path.Combine(baseUrl, container, ToPath(group, name)).Replace("\\", "/");
    }
}