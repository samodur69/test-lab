using NLog;

namespace Common.Logger.Impl;

public class FileLogger : ILogger
{
    private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();
    public void LOG_INFO(string message) => logger.Info(message);
    public void LOG_WARN(string message) => logger.Warn(message);
    public void LOG_DEBUG(string message) => logger.Debug(message);
    public void LOG_ERROR(string message) => logger.Error(message);
}