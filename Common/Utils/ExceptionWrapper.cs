namespace Common.Utils.ExceptionWrapper;

public static class ExceptionWrapper
{
    public static ExceptionData Test(Action action)
    {
        try
        {
            action();
            return new();
        }
        catch (Exception ex)
        {
            return new(Message : ex.Message, StackTrace : ex.StackTrace ?? "", Source : ex.Source ?? "");
        }
    }
}