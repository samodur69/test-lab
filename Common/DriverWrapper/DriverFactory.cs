namespace Common.DriverWrapper;

using Common.Enums;
using Common.DriverWrapper.Configuration;
using Common.DriverWrapper.Impl.Selenium;

public class DriverFactory
{
    public static IDriver Create(DriverConfig driverConfig)
    {
        return driverConfig.DriverType switch
        {
            DriverTypes.SELENIUM => new SeleniumDriver(driverConfig.WebDriverConfig),
            DriverTypes.PLAYWRIGHT => throw new NotImplementedException("PlayWright hasn't been implemented yet!"),
            _ => throw new ArgumentException("No/wrong driver was selected!")
        };
    }
};