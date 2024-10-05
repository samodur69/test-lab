namespace Common.Logger.Impl;

using NLog;

public class ConsoleLogger : Common.Logger.ILogger
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public void LOG(string message) => logger.Debug(message);
}