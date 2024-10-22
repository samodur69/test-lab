namespace Common.Utils.ExceptionWrapper;

public readonly struct ExceptionData(string Message = "", string StackTrace = "", string Source = "")
{
    public readonly string Message = Message;
    public readonly string StackTrace = StackTrace;
    public readonly string Source = Source;

    public bool IsError() => Message.Length > 0 || StackTrace.Length > 0 || Source.Length > 0;
}