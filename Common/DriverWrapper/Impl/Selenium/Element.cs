namespace Common.DriverWrapper.Impl.Selenium;

using System.Collections.ObjectModel;
using OpenQA.Selenium;

public class SeleniumElement(IWebElement element) : IElement
{
    private readonly string clearFieldKeyCombo = Keys.Control + "a" + Keys.Delete;
    private readonly IWebElement element = element;
    public string TagName => element.TagName;
    public string Text => element.Text;
    public bool Enabled => element.Enabled;
    public bool Selected => element.Selected;
    public bool Displayed => element.Displayed;
    public void Clear() => element.Clear();
    public void ClearViaKeys() => element.SendKeys(clearFieldKeyCombo);
    public void SendText(string text) => element.SendKeys(text);
    public void SendEnterKey() => element.SendKeys(Keys.Enter);
    public void Submit() => element.Submit();
    public void Click() => element.Click();

    public IElement FindElement(ILocator locator) => new SeleniumElement(element.FindElement(LocatorConverter.Convert(locator)));

    public IEnumerable<IElement> FindElements(ILocator locator) => element.FindElements(LocatorConverter.Convert(locator))
                 .Select(elem => (IElement)new SeleniumElement(elem))
                 .ToList();

    public IElement FindParent() => new SeleniumElement(element.FindElement(By.XPath("..")));

    public IEnumerable<IElement> FindChildElements() => element.FindElements(By.XPath("./*"))
                 .Select(elem => (IElement)new SeleniumElement(elem))
                 .ToList();

    public IElement FindElementByCss(string css) => FindElement(new SeleniumLocator(LocatorTypes.CSS_SELECTOR, css));
    public IElement FindElementByXPath(string xpath) => FindElement(new SeleniumLocator(LocatorTypes.XPATH, xpath));
    public string GetAttribute(string attributeName) => element.GetAttribute(attributeName);
    public string GetCssValue(string propertyName) => element.GetCssValue(propertyName);
    public string GetDomAttribute(string attributeName) => element.GetDomAttribute(attributeName);
    public string GetDomProperty(string propertyName) => element.GetDomProperty(propertyName);
};
