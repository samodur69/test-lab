namespace Common.DriverWrapper;

using Common.Enums;
using Common.DriverWrapper.Configuration;
using Common.DriverWrapper.Impl.Selenium;
using Common.DriverWrapper.Impl.Playwright;

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
};