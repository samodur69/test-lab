using Common.DriverWrapper.Configuration;
using Common.DriverWrapper.Impl.Playwright;
using Common.DriverWrapper.Impl.Selenium;
using Common.Enums;

namespace Common.DriverWrapper;

public static class DriverFactory
{
    public static IDriver Create(DriverConfig driverConfig)
    {
        return driverConfig.DriverType switch
        {
            DriverTypes.SELENIUM => new SeleniumDriver(driverConfig.WebDriverConfig),
            DriverTypes.PLAYWRIGHT => new PlaywrightDriver(driverConfig.WebDriverConfig),
            _ => throw new ArgumentException("No/wrong driver was selected!")
        };
    }
}