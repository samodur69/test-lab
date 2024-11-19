namespace Common.Utils.ExceptionWrapper;

public static class ExceptionWrapper
{
    public static ExceptionData Test(Action action)
    {
        try
        {
            action();
            return new ExceptionData(string.Empty, string.Empty, string.Empty);
        }
        catch (Exception ex)
        {
            return new ExceptionData(message : ex.Message ?? string.Empty, stackTrace : ex.StackTrace ?? string.Empty, source : ex.Source ?? string.Empty);
        }
    }
}