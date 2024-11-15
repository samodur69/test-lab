namespace Common.Logger;

public interface ILogger
{
    void LOG_INFO(string message);
    void LOG_WARN(string message);
    void LOG_DEBUG(string message);
    void LOG_ERROR(string message);
}