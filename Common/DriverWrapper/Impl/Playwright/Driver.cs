namespace Common.DriverWrapper.Impl.Playwright;

using AppDriver = Common.Driver;
using AppDriverSettings = Common.Driver.Configuration;

using Common.DriverWrapper;
using PW = Microsoft.Playwright;
using Common.Utils.Waiter;
using Common.Driver;

public class PlaywrightDriver(AppDriverSettings.WebDriverConfig settings) : IDriver
{
    private readonly AppDriver.PlaywrightManager.Context _context = AppDriver.PlaywrightManager.Create(settings);
    private readonly AppDriverSettings.WebDriverConfig _webDriverConfig = settings;


    public IElement FindElementByCss(string css, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") => 
        FindWaitOnElement(new PlaywrightLocator(LocatorTypes.CSS_SELECTOR, css), waitingStrategy, customArg0, customArg1);

    public IElement FindElementByXPath(string xpath, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") => 
        FindWaitOnElement(new PlaywrightLocator(LocatorTypes.XPATH, xpath), waitingStrategy, customArg0, customArg1);

    public IElement FindElement(ILocator locator, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") 
        => FindWaitOnElement(locator, waitingStrategy, customArg0, customArg1);

    public IEnumerable<IElement> FindElements(ILocator locator)
    {
        return _context.Page.Locator(LocatorConverter.Convert(locator, _context.Page)).AllAsync().Result
            .Select(loc => (IElement)new PlaywrightElement(loc.ElementHandleAsync().Result, _context.Page, locator))
            .ToList();
    }

    public IEnumerable<IElement> FindElementsByCss(string css)
    {
        var locator = new PlaywrightLocator().CssSelector(css);
        return _context.Page.Locator(LocatorConverter.Convert(locator, _context.Page)).AllAsync().Result
            .Select(loc => (IElement)new PlaywrightElement(loc.ElementHandleAsync().Result, _context.Page, locator))
            .ToList();
    }

    public IEnumerable<IElement> FindElementsByXPath(string xpath)
    {
        var locator = new PlaywrightLocator().XPath(xpath);
        return _context.Page.Locator(LocatorConverter.Convert(locator, _context.Page)).AllAsync().Result
            .Select(loc => (IElement)new PlaywrightElement(loc.ElementHandleAsync().Result, _context.Page, locator))
            .ToList();
    }

    private PlaywrightElement FindWaitOnElement(ILocator locator, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "") {
        PlaywrightElement? foundElement = null;
        Exception lastException = new();

        var newLoc = LocatorConverter.Convert(locator, _context.Page);

        static bool WaitCriteria(WaitingStrategy strat, string customArg0, string customArg1, PlaywrightElement elem) =>
            elem switch
            {
                _ when strat == WaitingStrategy.DISPLAYED && elem.Displayed => true,
                _ when strat == WaitingStrategy.ENABLED && elem.Enabled => true,
                _ when strat == WaitingStrategy.TEXT && string.Equals(elem.Text, customArg0, StringComparison.OrdinalIgnoreCase) => true,
                _ when strat == WaitingStrategy.ATTRIBUTE && string.Equals(elem.GetAttribute(customArg0), customArg1, StringComparison.OrdinalIgnoreCase) => true,
                _ when strat == WaitingStrategy.NONE => true,
                _ => false
            };
        
        bool WaitForElement()
        {
            try
            {
                PlaywrightElement? elem = null;
                
                if(!Waiter.WaitUntil(() =>
                   {
                       var loc = _context.Page.WaitForSelectorAsync(newLoc, new PW.PageWaitForSelectorOptions { State = PW.WaitForSelectorState.Attached }).Result;
                       if(loc == null) return false;

                       if(!WaitCriteria(waitingStrategy, customArg0, customArg1, new PlaywrightElement(loc, _context.Page, locator)))
                           return false;
                    
                       elem = new PlaywrightElement(loc, _context.Page, locator);
                       return true;
                   })) return false;
                
                foundElement = elem;
                return true;
            } catch (Exception ex)
            {
                lastException = ex;
                return false;
            }
        }

        return WaitForElement() ? foundElement! : throw lastException;
    }

    public void GoToURL(string url) => _context.Page.GotoAsync(url).Wait();
    public string GetURL() => _context.Page.Url;

    public void ScrollDown(int byAmount = -1)
    {
        var windowSize = _context.Page.ViewportSize;
        if (windowSize == null) return;

        _context.Page.Mouse.WheelAsync(0, (byAmount == -1) ? windowSize.Height : byAmount).Wait();
    }

    public bool SwitchToNextTab()
    {
        var windowHandles = _context.Page.Context.Pages;

        if (windowHandles.Count <= 1)
            return false;

        windowHandles[1].BringToFrontAsync().Wait();
        return true;
    }

    public string TakeScreenshot(string testName = "")
    {
        var screenshotPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _webDriverConfig.ScreenshotPath);
        Directory.CreateDirectory(screenshotPath);

        string fileName = $"{string.Join("_", testName.Split(Path.GetInvalidFileNameChars()))}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";
        _context.Page.ScreenshotAsync(new PW.PageScreenshotOptions { Path = Path.Combine(screenshotPath, fileName) }).Wait();

        return  Path.Combine(screenshotPath, fileName);
    }

    public ICollection<System.Net.Cookie> GetCookies()
    {
        return _context.Page.Context.CookiesAsync().Result
            .Select(cookie => new System.Net.Cookie
            {
                Name = cookie.Name,
                Value = cookie.Value,
                Domain = cookie.Domain,
                Path = cookie.Path,
                Secure = cookie.Secure,
                HttpOnly = cookie.HttpOnly
            })
            .ToList();
    }

    public void Refresh() => _context.Page.ReloadAsync().Wait();
    public void GoBack() => _context.Page.GoBackAsync().Wait();
    public void Close() => PlaywrightManager.Close();

    public void MoveCursorToElement(IElement element) => _context.Page.Mouse.MoveAsync((element as PlaywrightElement)!.Position.X, (element as PlaywrightElement)!.Position.Y).Wait();
    public void DragAndDropToOffset(IElement element, int x, int y)
    {
        var boundingBox =  (element as PlaywrightElement)!.ElementHandle.BoundingBoxAsync().Result;
        
        if (boundingBox != null)
        {

            _context.Page.Mouse.MoveAsync(boundingBox.X + boundingBox.Width / 2, boundingBox.Y + boundingBox.Height / 2).Wait();
            _context.Page.Mouse.DownAsync().Wait();
            _context.Page.Mouse.MoveAsync( boundingBox.X + boundingBox.Width / 2 + x, boundingBox.Y + boundingBox.Height / 2 + y).Wait();
            _context.Page.Mouse.UpAsync().Wait();
        }
    }
    public void Click(int x, int y) => _context.Page.Mouse.ClickAsync(x, y).Wait();
    public void Click(System.Drawing.Point pos) => _context.Page.Mouse.ClickAsync(pos.X, pos.Y).Wait();
    public void ClickElement(IElement element) => (element as PlaywrightElement)!.Click();
    public void RightClickElement(IElement element) => (element as PlaywrightElement)!.RightClick();
    public void DoubleClickElement(IElement element) => (element as PlaywrightElement)!.DoubleClick();
    public void DoubleClick(int x, int y) { _context.Page.Mouse.DblClickAsync(x, y).Wait(); }
    public void DoubleClick(System.Drawing.Point pos) { _context.Page.Mouse.DblClickAsync(pos.X, pos.Y).Wait(); }
    public void MoveCursorTo(System.Drawing.Point pos) => _context.Page.Mouse.MoveAsync(pos.X, pos.Y).Wait();
    public void MoveCursorTo(int x, int y) => _context.Page.Mouse.MoveAsync(x, y).Wait();
    public System.Drawing.Point GetElementPos(IElement element) => (element as PlaywrightElement)!.Position;
};
