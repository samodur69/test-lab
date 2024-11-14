namespace Common.Logger.Impl;

public class DummyLogger : Common.Logger.ILogger
{
    public void LOG_INFO(string message) {}
    public void LOG_WARN(string message) {}
    public void LOG_DEBUG(string message){}
    public void LOG_ERROR(string message){}
}