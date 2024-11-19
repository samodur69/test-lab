using ReportPortal.Shared.Execution;

namespace Common.Logger.Impl;

public class ReportPortalCustomLogger(ITestContext context) : ILogger
{
    private readonly ITestContext RPContext = context;
    public void LOG_INFO(string message) => RPContext.Log.Info(message);
    public void LOG_WARN(string message) => RPContext.Log.Warn(message);
    public void LOG_DEBUG(string message) => RPContext.Log.Debug(message);
    public void LOG_ERROR(string message) => RPContext.Log.Error(message);
}