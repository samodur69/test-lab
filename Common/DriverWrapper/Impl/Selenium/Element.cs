namespace Common.DriverWrapper.Impl.Selenium;

using OpenQA.Selenium;

public class SeleniumElement(IWebElement element, ILocator locator) : IElement
{
    public ILocator Locator { get; } = locator;
    private readonly string clearFieldKeyCombo = Keys.Control + "a" + Keys.Delete;
    public readonly IWebElement Element = element;
    public string TagName => Element.TagName;
    public string Text => Element.Text;
    public bool Enabled => Element.Enabled;
    public bool Selected => Element.Selected;
    public bool Displayed => Element.Displayed;
    public System.Drawing.Point Position => Element.Location;
    public void Clear() => Element.Clear();
    public void ClearViaKeys() => Element.SendKeys(clearFieldKeyCombo);
    public void SendText(string text) => Element.SendKeys(text);
    public void SendEnterKey() => Element.SendKeys(Keys.Enter);
    public void Submit() => Element.Submit();
    public void Click() => Element.Click();
    public System.Drawing.Size Size => Element.Size;

    public IElement FindElement(ILocator locator) => new SeleniumElement(Element.FindElement(LocatorConverter.Convert(locator)), locator);

    public IEnumerable<IElement> FindElements(ILocator locator) => Element.FindElements(LocatorConverter.Convert(locator))
                 .Select(elem => (IElement)new SeleniumElement(elem, locator))
                 .ToList();
    public IEnumerable<IElement> FindElementsByCss(string css) 
    {
        var loc = new SeleniumLocator().CssSelector(css);
        
        return Element.FindElements(LocatorConverter.Convert(loc))
                 .Select(elem => (IElement)new SeleniumElement(elem, loc))
                 .ToList();
    }
    public IEnumerable<IElement> FindElementsByXPath(string xpath) 
    {
        var loc = new SeleniumLocator().XPath(xpath);
        
        return Element.FindElements(LocatorConverter.Convert(loc))
                 .Select(elem => (IElement)new SeleniumElement(elem, loc))
                 .ToList();
    }

    public IElement FindParent() => new SeleniumElement(Element.FindElement(By.XPath("parent::*")), Locator.XPath("parent::*"));

    public IEnumerable<IElement> FindChildElements() => Element.FindElements(By.XPath("./*"))
                 .Select(elem => (IElement)new SeleniumElement(elem, Locator.XPath("./*")))
                 .ToList();

    public IElement FindElementByCss(string css) => FindElement(new SeleniumLocator(LocatorTypes.CSS_SELECTOR, css));
    public IElement FindElementByXPath(string xpath) => FindElement(new SeleniumLocator(LocatorTypes.XPATH, xpath));
    public string GetAttribute(string attributeName) => Element.GetAttribute(attributeName);
    public string GetCssValue(string propertyName) => Element.GetCssValue(propertyName);
    public string GetDomAttribute(string attributeName) => Element.GetDomAttribute(attributeName);
    public string GetDomProperty(string propertyName) => Element.GetDomProperty(propertyName);
    public System.Drawing.Point GetElementPos() => Element.Location;
};
