using System.Drawing;
using Microsoft.Playwright;

namespace Common.DriverWrapper.Impl.Playwright;

public class PlaywrightElement(IElementHandle elementHandle, IPage page, ILocator locator) : IElement
{
    public ILocator Locator { get; } = locator;
    public readonly IElementHandle ElementHandle = elementHandle;
    public string TagName => ElementHandle.EvaluateAsync<string>("el => el.tagName").Result;
    public string Text => ElementHandle.TextContentAsync().Result ?? "";
    public bool Enabled => ElementHandle.IsEnabledAsync().Result;
    public bool Selected => ElementHandle.EvaluateAsync<bool>("element => element === document.activeElement").Result;
    public bool Displayed => ElementHandle.IsVisibleAsync().Result;
    public Point Position
    {
        get 
        {
            var box = ElementHandle.BoundingBoxAsync().Result;
            return box == null
                ? new Point(0, 0)
                : new Point((int)box.X, (int)box.Y);
        }
    }

    public Size Size
    {
        get 
        {
            var box = ElementHandle.BoundingBoxAsync().Result;
            return box == null
                ? new Size(0, 0)
                : new Size((int)box.Width, (int)box.Height);
        }
    }

    public void Clear() => ElementHandle.FillAsync(string.Empty).Wait();

    public void ClearViaKeys()
    {
        ElementHandle.FocusAsync().Wait();
        ElementHandle.FillAsync(string.Empty).Wait();
    }

    public void SendText(string text) => ElementHandle.FillAsync(text).Wait();
    
    public void SendEnterKey() => ElementHandle.PressAsync("Enter").Wait();

    public void Submit() => ElementHandle.PressAsync("Enter").Wait();

    public void Click() => ElementHandle.ClickAsync().Wait();
    public void DoubleClick() => ElementHandle.DblClickAsync().Wait();
    public void RightClick() => ElementHandle.ClickAsync(new ElementHandleClickOptions { Button = MouseButton.Right }).Wait();

    public IElement FindElement(ILocator locator)
    {
        return new PlaywrightElement(ElementHandle.QuerySelectorAsync(LocatorConverter.Convert(locator)).Result!, page, locator);
    }

    public IEnumerable<IElement> FindElements(ILocator locator)
    {
        var elements = ElementHandle.QuerySelectorAllAsync(LocatorConverter.Convert(locator)).Result!;
        return elements.Select(IElement (elem) => new PlaywrightElement(elem, page, locator)).ToList();
    }

    public IEnumerable<IElement> FindElementsByCss(string css)
    {
        var loc = new PlaywrightLocator().CssSelector(css);
        var elements = ElementHandle.QuerySelectorAllAsync(LocatorConverter.Convert(loc)).Result!;
        return elements.Select(IElement (elem) => new PlaywrightElement(elem, page, loc)).ToList();
    }

    public IEnumerable<IElement> FindElementsByXPath(string xpath)
    {
        var loc = new PlaywrightLocator().XPath(xpath);
        var elements = ElementHandle.QuerySelectorAllAsync(LocatorConverter.Convert(loc)).Result!;
        return elements.Select(IElement (elem) => new PlaywrightElement(elem, page, loc)).ToList();
    }

    public IElement FindParent()
    {  
        var parentElement = ElementHandle.QuerySelectorAsync("xpath=//parent::*").Result ?? 
            throw new InvalidOperationException("FindParent() failed to find parent element.");
        return new PlaywrightElement(parentElement, page, Locator.CssSelector("xpath=//parent::*"));
    }

    public IEnumerable<IElement> FindChildElements()
    {
        var childElements = ElementHandle.QuerySelectorAllAsync("xpath=./*").Result;
        return childElements.Select(IElement (elem) => new PlaywrightElement(elem, page, Locator.CssSelector("xpath=./*"))).ToList();
    }

    public IElement FindElementByCss(string css) => FindElement(new PlaywrightLocator(LocatorTypes.CSS_SELECTOR, css));
    public IElement FindElementByXPath(string xpath) => FindElement(new PlaywrightLocator(LocatorTypes.XPATH, xpath));

    public string GetAttribute(string attributeName) => ElementHandle.GetAttributeAsync(attributeName).Result ?? string.Empty;
    public string GetCssValue(string propertyName) => ElementHandle.EvaluateAsync<string>($"(el) => window.getComputedStyle(el).getPropertyValue('{propertyName}')", ElementHandle).Result;
    public string GetDomAttribute(string attributeName) => ElementHandle.GetAttributeAsync(attributeName).Result ?? string.Empty;
    public string GetDomProperty(string propertyName) => ElementHandle.EvaluateAsync<string>($"(el) => el.{propertyName}", ElementHandle).Result;
}