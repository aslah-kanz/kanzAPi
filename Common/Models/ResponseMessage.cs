namespace KanzApi.Common.Models;

public class ResponseMessage<T>(string code)
{

    private string _code = code;
    public string Code { get { return _code; } set { _code = value; } }

    private string? _message;
    public string? Message { get { return _message; } set { _message = value; } }

    private T? _data;
    public T? Data { get { return _data; } set { _data = value; } }

    private DateTime _timestamp = DateTime.Now;
    public DateTime Timestamp { get { return _timestamp; } set { _timestamp = value; } }

    public static ResponseMessage<T> Success(T data)
    {
        return new("0")
        {
            Data = data
        };
    }

    public static ResponseMessage<T> Error(string code, string? message, T? data)
    {
        return new(code)
        {
            Message = message,
            Data = data
        };
    }
}
