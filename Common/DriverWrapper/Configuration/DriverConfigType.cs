using Common.Enums;

namespace Common.DriverWrapper.Configuration;

using AppDriver = Driver.Configuration;

public record DriverConfig
{
    public DriverConfig(string driverType, AppDriver.WebDriverConfig driverConfig)
    {
        Enum.TryParse(driverType, true, out DriverType);
        WebDriverConfig = driverConfig;
    }
    public readonly DriverTypes DriverType = DriverTypes.SELENIUM;
    public readonly AppDriver.WebDriverConfig WebDriverConfig = new();
}