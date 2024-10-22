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

    public static IWebDriver Create(WebDriverConfig settings)
    {
        if (driver.Value != null) return driver.Value;

        driver.Value = settings.DriverType switch
        {
            WebDriverTypes.CHROME => new ChromeDriver(SetChromiumOptions(settings.DriverSettings)),
            WebDriverTypes.EDGE => new EdgeDriver(SetEdgeOptions(settings.DriverSettings)),
            WebDriverTypes.FIREFOX => new FirefoxDriver(SetFireFoxOptions(settings.DriverSettings)),
            _ => throw new ArgumentException($"SeleniumDriverManager - browser type: {settings.DriverType} is not supported!")
        };

        return driver.Value;
    }
    public static void GoToURL(string url) => driver.Value?.Navigate().GoToUrl(url);
    public static string? GetURL() => driver.Value?.Url;
    public static void Close()
    {
        driver.Value?.Dispose();
        driver.Value = null;
    }

    private static ChromeOptions SetChromiumOptions(WebBrowserSettings settings)
    {
        var options = new ChromeOptions();
        if (settings.Maximized)
        {
            options.AddArgument("--start-maximized");
        }

        return options;
    }

    private static FirefoxOptions SetFireFoxOptions(WebBrowserSettings settings)
    {
        var options = new FirefoxOptions();
        if (settings.Maximized)
        {
            options.AddArgument("--start-maximized");
        }

        return options;
    }

    private static EdgeOptions SetEdgeOptions(WebBrowserSettings settings)
    {
        var options = new EdgeOptions();
        if (settings.Maximized)
        {
            options.AddArgument("--start-maximized");
        }

        return options;
    }
}
