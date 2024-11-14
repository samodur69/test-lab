namespace Common.DriverWrapper;

using Common.Enums;
using Common.DriverWrapper.Configuration;
using Common.DriverWrapper.Impl.Selenium;
using Common.DriverWrapper.Impl.Playwright;

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
};