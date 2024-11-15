namespace Common.Logger.Impl;

using NLog;

public class ConsoleLogger : Common.Logger.ILogger
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public void LOG_INFO(string message) => logger.Info(message);
    public void LOG_WARN(string message) => logger.Warn(message);
    public void LOG_DEBUG(string message) => logger.Debug(message);
    public void LOG_ERROR(string message) => logger.Error(message);
}