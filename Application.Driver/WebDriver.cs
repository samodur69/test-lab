using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.BiDi.Communication;
using OpenQA.Selenium.Interactions;

namespace Application.Driver
{
    public struct WebDriverSettings
    {
        public string Browser { get; }
        public bool IsMaximized { get; }

        public WebDriverSettings(string browser, bool isMaximized)
        {
            Browser = browser;
            IsMaximized = isMaximized;
        }
    }
    public class WebDriver
    {
        private IWebDriver? _driver;

        public IWebDriver CreateDriver(WebDriverSettings settings)
        {
            switch (settings.Browser)
            {
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    SetDriverOptions(chromeOptions, settings);
                    _driver = new ChromeDriver(chromeOptions);
                    break;

                case "Edge":
                    var edgeOptions = new EdgeOptions();
                    SetDriverOptions(edgeOptions, settings);
                    _driver = new EdgeDriver(edgeOptions);
                    break;

                case "Firefox":
                    var firefoxOptions = new FirefoxOptions();
                    SetDriverOptions(firefoxOptions, settings);
                    _driver = new FirefoxDriver(firefoxOptions);
                    break;

                default:
                    throw new ArgumentException("Error: Incorrect name of browser");
            }
            return _driver;
        }
        private void SetDriverOptions(dynamic options,WebDriverSettings settings)
        {
            if (settings.IsMaximized)
            {
                options.AddArgument("--start-maximized");
            }
        }
        public void GoToURL(string url)
        {
            _driver?.Navigate().GoToUrl(url);
        }
        public string? GetURL()
        {
            return _driver?.Url;
        }
        public void QuitDriver()
        {
            _driver?.Quit();
        }
    }
}
