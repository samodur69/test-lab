using Common.Logger.Configuration;
using Common.Logger.Impl;
using NLog;
using NLog.Config;
using NLog.Targets;
using ReportPortal.Shared.Execution;

namespace Common.Logger;

public static class LoggerFactory
{
    public static ILogger Create(LoggerConfig logConfig, object? extraArg = null)
    {
        ApplyConfig(logConfig);

        return logConfig.LoggerType switch
        {
            LoggerType.FILE => new FileLogger(),
            LoggerType.CONSOLE => new ConsoleLogger(),
            LoggerType.REPORT_PORTAL => new ReportPortalCustomLogger((ITestContext)extraArg!),
            _ => new DummyLogger()
        };
    }

    private static void ApplyConfig(LoggerConfig logConfig)
    {
        var config = new LoggingConfiguration();

        if(logConfig.LoggerType == LoggerType.FILE) {
            if(logConfig.LogFileName.Length == 0) throw new ArgumentException("LoggerFactory.ApplyConfig log file name cannot be empty!", nameof(logConfig));

            var logfile = new FileTarget("logfile") { FileName = logConfig.LogFileName };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
        } else if(logConfig.LoggerType == LoggerType.CONSOLE) {
            var logconsole = new ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
        }

        LogManager.Configuration = config;
    }
}