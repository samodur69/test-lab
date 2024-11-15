namespace Common.Utils.ExceptionWrapper;

public readonly struct ExceptionData(string message, string stackTrace, string source)
{
    public readonly string Message = message;
    public readonly string StackTrace = stackTrace;
    public readonly string Source = source;

    public bool IsError() => Message.Length > 0 || StackTrace.Length > 0 || Source.Length > 0;
}