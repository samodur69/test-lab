using Common.DriverWrapper;
using Common.DriverWrapper.Configuration;
using Common.Configuration;
using Common.Driver.Configuration;

namespace Application.Model;

public class ModelBase
{
    public const int DEFAULT_TIMEOUT = 1000;
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    private static readonly ThreadLocal<IDriver> driver = new();
    protected static IDriver Driver { get => driver.Value!; }

    public ModelBase() {
        driver.Value = DriverFactory.Create(
            new DriverConfig(
                AppConfig.DriverOptions.Type, 
                new WebDriverConfig(
                   AppConfig.BrowserOptions.Browser,
                   AppConfig.DriverOptions.WaitTimeout,
                   AppConfig.DriverOptions.PollingRate,
                   AppConfig.DriverOptions.ScreenshotDir,
                    new WebBrowserSettings(
                        Maximized : AppConfig.BrowserOptions.Maximize,
                        Headless : AppConfig.BrowserOptions.Headless,
                        DisableSandbox : AppConfig.BrowserOptions.DisableSandbox,
                        DisableGPU : AppConfig.BrowserOptions.DisableGPU,
                        DisableSharedMemory : AppConfig.BrowserOptions.DisableSharedMemory,
                        EnableWindowSize : AppConfig.BrowserOptions.EnableWindowSize,
                        WindowSizeX : AppConfig.BrowserOptions.WindowSizeX,
                        WindowSizeY : AppConfig.BrowserOptions.WindowSizeY,
                        DebuggerAddress : AppConfig.BrowserOptions.DebuggerAddress,
                        DebuggerPort : AppConfig.BrowserOptions.DebuggerPort,
                        RemoteDebuggingPort : AppConfig.BrowserOptions.RemoteDebuggingPort
                    )
                )));
    }

    public string Url { get; set; } = "";
    public static string GetCurrentUrl() => Driver.GetURL();

    public void OpenUrl() => Driver.GoToURL(Url);

    public static ICollection<System.Net.Cookie> Cookies => Driver.GetCookies();

    public static void Refresh() => Driver.Refresh();

    public static void BackClick() => Driver.GoBack();

    public static string TakeScreenshot(string name = "") => Driver.TakeScreenshot(name);

    public static void CloseBrowser() => Driver.Close();
}
