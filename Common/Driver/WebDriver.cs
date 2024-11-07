namespace Common.Driver;

using Common.Driver.Configuration;
using Common.Enums;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chromium;

public static class SeleniumDriverManager
{
    private static readonly ThreadLocal<IWebDriver?> Driver = new();

    public static IWebDriver Create(WebDriverConfig settings)
    {
        if (Driver.Value != null) return Driver.Value;
        
        Driver.Value = settings.DriverType switch
        {
            WebDriverTypes.CHROME => new ChromeDriver((ChromeDriverService)CreateChromiumService(WebDriverTypes.CHROME, settings.DriverSettings), SetChromiumOptions<ChromeOptions>(settings.DriverSettings)),
            WebDriverTypes.EDGE => new EdgeDriver((EdgeDriverService)CreateChromiumService(WebDriverTypes.EDGE, settings.DriverSettings), SetChromiumOptions<EdgeOptions>(settings.DriverSettings)),
            WebDriverTypes.FIREFOX => SetGeckoOptions(settings.DriverSettings),
            _ => throw new ArgumentException($"SeleniumDriverManager - browser type: {settings.DriverType} is not supported!")
        };

        return Driver.Value;
    }
    public static void GoToUrl(string url) => Driver.Value?.Navigate().GoToUrl(url);
    public static string? GetUrl() => Driver.Value?.Url;
    public static void Close()
    {
        Driver.Value?.Dispose();
        Driver.Value = null;
    }

    private static ChromiumDriverService CreateChromiumService(WebDriverTypes type, WebBrowserSettings settings)
    {
        ChromiumDriverService service = type switch
        {
            WebDriverTypes.CHROME => ChromeDriverService.CreateDefaultService(),
            WebDriverTypes.EDGE => EdgeDriverService.CreateDefaultService(),
            WebDriverTypes.FIREFOX => throw new NotImplementedException(),
            _ => throw new ArgumentException($"SeleniumDriverManager - CreateChromiumService - browser type: {type} is not supported!")
        };

        if(settings.RemoteDebuggingPort > 0)
            service.Port = settings.RemoteDebuggingPort;

        return service;
    }

    private static T SetChromiumOptions<T>(WebBrowserSettings settings) where T : ChromiumOptions, new()
    {
        var options = new T();
        if (settings.Maximized)
            options.AddArgument("--start-maximized");
        if(settings.Headless)
            options.AddArgument("--headless");
        if(settings.DisableSandbox)
            options.AddArgument("--no-sandbox");
        if(settings.DisableGPU)
            options.AddArgument("--disable-gpu");
        if(settings.DisableSharedMemory)
            options.AddArgument("--disable-dev-shm-usage");
        if(settings.EnableWindowSize)
            options.AddArgument($"--window-size={settings.WindowSizeX},{settings.WindowSizeY}");
        if(settings.DebuggerAddress.Length > 0 && settings.DebuggerPort > 0)
            options.DebuggerAddress = $"{settings.DebuggerAddress}:{settings.DebuggerPort}";
        if(settings.DebuggerAddress.Length > 0 && settings.RemoteDebuggingPort > 0)
        {
            options.AddArgument($"--remote-debugging-port={settings.RemoteDebuggingPort}");
            options.AddArgument($"--remote-debugging-address={settings.DebuggerAddress}");
        }

        return options;
    }

    private static FirefoxDriver SetGeckoOptions(WebBrowserSettings settings)
    {
        var options = new FirefoxOptions();
        var drv = new FirefoxDriver(options);

        if (settings.Maximized)
        {
            drv.Manage().Window.Maximize();
        }

        return drv;
    }
}
