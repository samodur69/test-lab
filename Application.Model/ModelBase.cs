using Common.DriverWrapper;
using Common.DriverWrapper.Configuration;
using Common.Configuration;
using Common.Driver.Configuration;

namespace Application.Model;

public class ModelBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    private static ThreadLocal<IDriver> driver = new();
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
                        Maximized : AppConfig.BrowserOptions.Maximize
                    )
                )));
    }

    public string Url { get; set; } = "";

    public void OpenUrl() => Driver.GoToURL(Url);

    public ICollection<System.Net.Cookie> Cookies => Driver.GetCookies();

    public static void Refresh() => Driver.Refresh();

    public static void BackClick() => Driver.GoBack();

    public static string TakeScreenshot(string name = "") => Driver.TakeScreenshot(name);

    public static void CloseBrowser() => Driver.Close();
}
