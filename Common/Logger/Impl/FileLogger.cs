namespace Common.Logger.Impl;

using NLog;

public class FileLogger : Common.Logger.ILogger
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public void LOG(string message)
    {
        logger.Info(message);
    }
}