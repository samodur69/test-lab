namespace Common.Driver.Configuration;

using Common.Enums;

public record WebDriverConfig
{
    public readonly WebDriverTypes DriverType = WebDriverTypes.CHROME;
    public readonly int WaitTimeout = 5000;
    public readonly int PollingRate = 500;
    public readonly string ScreenshotPath = "";
    public readonly WebBrowserSettings DriverSettings = new();

    public WebDriverConfig() {}

    public WebDriverConfig(string driverType)
    {
        Enum.TryParse(driverType, true, out DriverType);
    }

    public WebDriverConfig(string driverType, int waitTimeout, int pollingRate, string screenshotPath, WebBrowserSettings driverSettings)
    {
        Enum.TryParse(driverType, true, out DriverType);
        DriverSettings = driverSettings;
        WaitTimeout = waitTimeout;
        PollingRate = pollingRate;
        ScreenshotPath = screenshotPath;
    }
}