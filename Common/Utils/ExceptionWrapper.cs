namespace Common.Utils.ExceptionWrapper;

public static class ExceptionWrapper
{
    public static ExceptionData Test(Action action)
    {
        try
        {
            action();
            return new("", "", "");
        }
        catch (Exception ex)
        {
            return new(message : ex.Message ?? "", stackTrace : ex.StackTrace ?? "", source : ex.Source ?? "");
        }
    }
}