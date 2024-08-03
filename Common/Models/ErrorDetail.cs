namespace KanzApi.Common.Models;

public class ErrorDetail
{

    public string? Type { get; set; }

    public string? Message { get; set; }

    public IEnumerable<string>? StackTraces { get; set; }

    public static ErrorDetail From(Exception e)
    {
        return new()
        {
            Type = e.GetType().FullName,
            Message = e.Message,
            StackTraces = e.StackTrace?.Split("\n").Select(p => p.Trim())
        };

    }
}
