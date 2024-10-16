namespace Common.DriverWrapper;

using Common.Enums;
using Common.DriverWrapper.Configuration;
using Common.DriverWrapper.Impl.Selenium;

public class LocatorFactory
{
    public static ILocator Create(DriverConfig driverConfig)
    {
        return driverConfig.DriverType switch
        {
            DriverTypes.SELENIUM => new SeleniumLocator(),
            DriverTypes.PLAYWRIGHT => throw new NotImplementedException("PlayWright hasn't been implemented yet!"),
            _ => throw new ArgumentException("No/wrong driver was selected!")
        };
    }
};