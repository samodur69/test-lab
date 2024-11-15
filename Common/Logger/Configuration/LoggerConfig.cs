namespace Common.Logger.Configuration;

public record LoggerConfig
{
    public LoggerConfig(string loggerType, string fileName = "")
    {
        Enum.TryParse(loggerType, true, out LoggerType);
        LogFileName = fileName;
    }
    public readonly LoggerType LoggerType = LoggerType.NONE;
    public readonly string LogFileName = "";
}