namespace Common.Logger;
using Common.Logger.Impl;
using Common.Logger.Configuration;
using NLog;

public static class LoggerFactory
{
    public static ILogger Create(LoggerConfig logConfig, object? extraArg = null)
    {
        ApplyConfig(logConfig);

        return logConfig.LoggerType switch
        {
            LoggerType.FILE => new FileLogger(),
            LoggerType.CONSOLE => new ConsoleLogger(),
            LoggerType.REPORT_PORTAL => new ReportPortalCustomLogger((ReportPortal.Shared.Execution.ITestContext)extraArg!),
            _ => new DummyLogger()
        };
    }

    private static void ApplyConfig(LoggerConfig logConfig)
    {
        var config = new NLog.Config.LoggingConfiguration();

        if(logConfig.LoggerType == LoggerType.FILE) {
            if(logConfig.LogFileName.Length == 0) throw new ArgumentException("LoggerFactory.ApplyConfig log file name cannot be empty!", nameof(logConfig));

            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = logConfig.LogFileName };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
        } else if(logConfig.LoggerType == LoggerType.CONSOLE) {
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);
        }

        NLog.LogManager.Configuration = config;
    }
}