using Common.DriverWrapper.Configuration;
using Common.DriverWrapper.Impl.Playwright;
using Common.DriverWrapper.Impl.Selenium;
using Common.Enums;

namespace Common.DriverWrapper;

public static class LocatorFactory
{
    public static ILocator Create(DriverConfig driverConfig)
    {
        return driverConfig.DriverType switch
        {
            DriverTypes.SELENIUM => new SeleniumLocator(),
            DriverTypes.PLAYWRIGHT => new PlaywrightLocator(),
            _ => throw new ArgumentException("No/wrong driver was selected!")
        };
    }
}