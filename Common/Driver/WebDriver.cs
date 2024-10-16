namespace Common.Driver;

using Common.Driver.Configuration;
using Common.Enums;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

public class SeleniumDriverManager
{
    protected static readonly ThreadLocal<IWebDriver?> driver = new();

    public IWebDriver Create(WebDriverConfig settings)
    {
        if (driver.Value != null) return driver.Value;

        switch (settings.DriverType)
        {
            case WebDriverTypes.CHROME:
                driver.Value = new ChromeDriver(SetChromiumOptions(settings.DriverSettings));
                break;
            case WebDriverTypes.EDGE:
                driver.Value = new EdgeDriver(SetEdgeOptions(settings.DriverSettings));
                break;
            case WebDriverTypes.FIREFOX:
                driver.Value = new FirefoxDriver(SetFireFoxOptions(settings.DriverSettings));
                break;
            default:
                throw new ArgumentException($"SeleniumDriverManager - browser type: {settings.DriverType} is not supported!");
        }
        return driver.Value;
    }
    public void GoToURL(string url)
    {
        driver.Value?.Navigate().GoToUrl(url);
    }
    public string? GetURL()
    {
        return driver.Value?.Url;
    }
    public void Close()
    {
        driver.Value?.Dispose();
        driver.Value = null;
    }

    private ChromeOptions SetChromiumOptions(WebBrowserSettings settings)
    {
        var options = new ChromeOptions();
        if (settings.Maximized)
        {
            options.AddArgument("--start-maximized");
        }

        return options;
    }

    private FirefoxOptions SetFireFoxOptions(WebBrowserSettings settings)
    {
        var options = new FirefoxOptions();
        if (settings.Maximized)
        {
            options.AddArgument("--start-maximized");
        }

        return options;
    }

    private EdgeOptions SetEdgeOptions(WebBrowserSettings settings)
    {
        var options = new EdgeOptions();
        if (settings.Maximized)
        {
            options.AddArgument("--start-maximized");
        }

        return options;
    }
}
