namespace Common.DriverWrapper.Impl.Playwright;

using Microsoft.Playwright;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PlaywrightElement(IElementHandle elementHandle, IPage page, Common.DriverWrapper.ILocator locator) : IElement
{
    public Common.DriverWrapper.ILocator Locator { get; } = locator;
    public readonly IElementHandle ElementHandle = elementHandle;
    private readonly IPage _page = page;
    public string TagName => ElementHandle.EvaluateAsync<string>("el => el.tagName").Result;
    public string Text => ElementHandle.TextContentAsync().Result ?? "";
    public bool Enabled => ElementHandle.IsEnabledAsync().Result;
    public bool Selected => ElementHandle.EvaluateAsync<bool>("element => element === document.activeElement").Result;
    public bool Displayed => ElementHandle.IsVisibleAsync().Result;
    public System.Drawing.Point Position
    {
        get 
        {
            var box = ElementHandle.BoundingBoxAsync().Result;
            if(box == null ) return new System.Drawing.Point(0, 0);

            return new System.Drawing.Point((int)box.X, (int)box.Y);
        }
    }

    public System.Drawing.Size Size
    {
        get 
        {
            var box = ElementHandle.BoundingBoxAsync().Result;
            if(box == null ) return new System.Drawing.Size(0, 0);

            return new System.Drawing.Size((int)box.Width, (int)box.Height);
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
    public void RightClick() => ElementHandle.ClickAsync(new() { Button = MouseButton.Right }).Wait();

    public IElement FindElement(Common.DriverWrapper.ILocator locator)
    {
        return new PlaywrightElement(ElementHandle.QuerySelectorAsync(LocatorConverter.Convert(locator, _page)).Result!, _page, locator);
    }

    public IEnumerable<IElement> FindElements(Common.DriverWrapper.ILocator locator)
    {
        var elements = ElementHandle.QuerySelectorAllAsync(LocatorConverter.Convert(locator, _page)).Result!;
        return elements.Select(elem => (IElement)new PlaywrightElement(elem, _page, locator)).ToList();
    }

    public IEnumerable<IElement> FindElementsByCss(string css)
    {
        var loc = new PlaywrightLocator().CssSelector(css);
        var elements = ElementHandle.QuerySelectorAllAsync(LocatorConverter.Convert(loc, _page)).Result!;
        return elements.Select(elem => (IElement)new PlaywrightElement(elem, _page, loc)).ToList();
    }

    public IEnumerable<IElement> FindElementsByXPath(string xpath)
    {
        var loc = new PlaywrightLocator().XPath(xpath);
        var elements = ElementHandle.QuerySelectorAllAsync(LocatorConverter.Convert(loc, _page)).Result!;
        return elements.Select(elem => (IElement)new PlaywrightElement(elem, _page, loc)).ToList();
    }

    public IElement FindParent()
    {  
        var parentElement = ElementHandle.QuerySelectorAsync("xpath=//parent::*").Result ?? 
            throw new InvalidOperationException("FindParent() failed to find parent element.");
        return new PlaywrightElement(parentElement, _page, Locator.CssSelector("xpath=//parent::*"));
    }

    public IEnumerable<IElement> FindChildElements()
    {
        var childElements = ElementHandle.QuerySelectorAllAsync("xpath=./*").Result;
        return childElements.Select(elem => (IElement)new PlaywrightElement(elem, _page, Locator.CssSelector("xpath=./*"))).ToList();
    }

    public IElement FindElementByCss(string css) => FindElement(new PlaywrightLocator(LocatorTypes.CSS_SELECTOR, css));

    public IElement FindElementByXPath(string xpath) => FindElement(new PlaywrightLocator(LocatorTypes.XPATH, xpath));

    public string GetAttribute(string attributeName) => ElementHandle.GetAttributeAsync(attributeName).Result ?? "";

    public string GetCssValue(string propertyName) => ElementHandle.EvaluateAsync<string>($"(el) => window.getComputedStyle(el).getPropertyValue('{propertyName}')", ElementHandle).Result;

    public string GetDomAttribute(string attributeName) => ElementHandle.GetAttributeAsync(attributeName).Result ?? "";

    public string GetDomProperty(string propertyName) => ElementHandle.EvaluateAsync<string>($"(el) => el.{propertyName}", ElementHandle).Result;

    public System.Drawing.Point GetElementPos()
    {
        var box = ElementHandle.BoundingBoxAsync().Result;
        if(box == null ) return new System.Drawing.Point(0, 0);

        return new System.Drawing.Point((int)box.X, (int)box.Y);
    }
};