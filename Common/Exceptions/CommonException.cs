using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public abstract class CommonException(Exception? innerException, ErrorCode code, params object[] args)
: Exception(null, innerException)
{

    private ErrorCode _code = code;
    public ErrorCode Code { get { return _code; } set { _code = value; } }

    private object[] _args = args;
    public object[] Args { get { return _args; } set { _args = value; } }

    private object? _dataObject;
    public object? DataObject { get { return _dataObject; } set { _dataObject = value; } }

    public CommonException(ErrorCode code, params object[] args) : this(null, code, args) { }
}
