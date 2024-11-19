using System.Drawing;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Cookie = System.Net.Cookie;

namespace Common.DriverWrapper.Impl.Selenium;

using AppDriver = Driver;
using AppDriverSettings = Driver.Configuration;

public class SeleniumDriver(AppDriverSettings.WebDriverConfig settings) : IDriver
{
    private readonly IWebDriver _driver = AppDriver.SeleniumDriverManager.Create(settings);

    public IElement FindElementByCss(string css, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") => 
        FindWaitOnElement(new SeleniumLocator(LocatorTypes.CSS_SELECTOR, css), waitingStrategy, customArg0, customArg1);
    public IElement FindElementByXPath(string xpath, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") => 
        FindWaitOnElement(new SeleniumLocator(LocatorTypes.XPATH, xpath), waitingStrategy, customArg0, customArg1);

    public IElement FindElement(ILocator locator, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") 
        => FindWaitOnElement(locator, waitingStrategy, customArg0, customArg1);
    public IEnumerable<IElement> FindElements(ILocator locator) => _driver.FindElements(LocatorConverter.Convert(locator))
                 .Select(IElement (elem) => new SeleniumElement(elem, locator))
                 .ToList();

    public IEnumerable<IElement> FindElementsByCss(string css) 
    {
        var locator = new SeleniumLocator().CssSelector(css);
        
        return _driver.FindElements(LocatorConverter.Convert(locator))
                 .Select(IElement (elem) => new SeleniumElement(elem, locator))
                 .ToList();
    }
    public IEnumerable<IElement> FindElementsByXPath(string xpath) 
    {
        var locator = new SeleniumLocator().XPath(xpath);
        
        return _driver.FindElements(LocatorConverter.Convert(locator))
                 .Select(IElement (elem) => new SeleniumElement(elem, locator))
                 .ToList();
    }

    private SeleniumElement FindWaitOnElement(ILocator locator, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") {
        IWebElement? foundElement = null;
        Exception lastException = new();

        static bool waitCriteria(WaitingStrategy strat, string customArg0, string customArg1, IWebElement elem) =>
            elem switch
            {
                _ when strat == WaitingStrategy.DISPLAYED && elem.Displayed => true,
                _ when strat == WaitingStrategy.ENABLED && elem.Enabled => true,
                _ when strat == WaitingStrategy.TEXT && string.Equals(elem.Text, customArg0, StringComparison.OrdinalIgnoreCase) => true,
                _ when strat == WaitingStrategy.ATTRIBUTE && string.Equals(elem.GetAttribute(customArg0), customArg1, StringComparison.OrdinalIgnoreCase) => true,
                _ when strat == WaitingStrategy.NONE => true,
                _ => false
            };

        return new SeleniumElement(
            new WebDriverWait(
                _driver,
                TimeSpan.FromMilliseconds(settings.WaitTimeout))
                { PollingInterval = TimeSpan.FromMilliseconds(settings.PollingRate) }.Until
        (
            driver =>
            {
                try
                {
                    if(driver.FindElement(LocatorConverter.Convert(locator)) is var elem && waitCriteria(waitingStrategy, customArg0, customArg1, elem))
                    {
                        foundElement = elem;
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    return false;
                }
            }
        ) ? foundElement! : throw lastException, locator);
    }
    public void GoToURL(string url) => _driver.Navigate().GoToUrl(url);
    public string GetURL() => _driver.Url;
    public void ScrollDown(int byAmount = -1) {
        var windowSize = _driver.Manage().Window.Size;

        new Actions(_driver)
            .ScrollByAmount(0, (byAmount == -1) ? windowSize.Height : byAmount)
            .Perform();
    }

    public bool SwitchToNextTab() {
        var windowHandles = new List<string>(_driver.WindowHandles);
            if(windowHandles.Count <= 1)
                return false;

            _driver.SwitchTo().Window(windowHandles[1]);
            return true;
    }

    public string TakeScreenshot(string testName = "")
    {
        var screenshotDriver = _driver as ITakesScreenshot ?? throw new InvalidOperationException("The Selenium driver does not support taking screenshots");
        var screenshot = screenshotDriver.GetScreenshot();

        var screenshotDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, settings.ScreenshotPath);
        Directory.CreateDirectory(screenshotDirectory);
        
        var screenshotPath = Path.Combine(screenshotDirectory, $"{string.Join("_", testName.Split(Path.GetInvalidFileNameChars()))}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png");
        screenshot.SaveAsFile(screenshotPath);

        return screenshotPath;
    }

    public ICollection<Cookie> GetCookies()
    {
        CookieCollection netCookieCollection = [];

        _driver.Manage().Cookies.AllCookies
                 .Select(ConvertToNetCookie)
                 .ToList()
                 .ForEach(netCookie => netCookieCollection.Add(netCookie));

        return netCookieCollection;
    }

    public void Refresh() => _driver.Navigate().Refresh();
    
    public void GoBack() => _driver.Navigate().Back();

    public void Close() => AppDriver.SeleniumDriverManager.Close();
    private static Cookie ConvertToNetCookie(OpenQA.Selenium.Cookie seleniumCookie)
    {
        var netCookie = new Cookie
        {
            Name = seleniumCookie.Name,
            Value = seleniumCookie.Value,
            Domain = seleniumCookie.Domain,
            Path = seleniumCookie.Path,
            Secure = seleniumCookie.Secure,
            HttpOnly = seleniumCookie.IsHttpOnly
        };

        if (seleniumCookie.Expiry.HasValue)
        {
            netCookie.Expires = seleniumCookie.Expiry.Value;
        }

        return netCookie;
    }

    public void MoveCursorToElement(IElement element) => new Actions(_driver).MoveToElement((element as SeleniumElement)!.Element).Perform();
    public void DragAndDropToOffset(IElement element, int x, int y) => new Actions(_driver).DragAndDropToOffset((element as SeleniumElement)!.Element, x, y).Perform();
    public void Click(int x, int y) { MoveCursorTo(x, y); new Actions(_driver).Click().Perform(); }
    public void Click(Point pos) { MoveCursorTo(pos); new Actions(_driver).Click().Perform(); }
    public void ClickElement(IElement element) => new Actions(_driver).Click((element as SeleniumElement)!.Element).Perform();
    public void RightClickElement(IElement element) => new Actions(_driver).ContextClick((element as SeleniumElement)!.Element).Perform();
    public void DoubleClickElement(IElement element) => new Actions(_driver).DoubleClick((element as SeleniumElement)!.Element).Perform();
    public void DoubleClick(int x, int y) { MoveCursorTo(x, y); new Actions(_driver).DoubleClick().Perform(); }
    public void DoubleClick(Point pos) { MoveCursorTo(pos); new Actions(_driver).DoubleClick().Perform(); }
    public void MoveCursorTo(Point pos) => new Actions(_driver).MoveToLocation(pos.X, pos.Y).Perform();
    public void MoveCursorTo(int x, int y) => new Actions(_driver).MoveToLocation(x, y).Perform();
    public Point GetElementPos(IElement element) => (element as SeleniumElement)!.Element.Location;
}
