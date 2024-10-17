namespace Common.DriverWrapper;

public interface IElement
{
    public string TagName { get; }
    public string Text { get; }
    public bool Enabled { get; }
    public bool Selected { get; }
    public bool Displayed { get; }
    public void Clear();
    public void ClearViaKeys();
    public void SendText(string text);
    public void SendEnterKey();
    public void Submit();
    public void Click();
    public IElement FindElementByCss(string css);
    public IElement FindElementByXPath(string xpath);
    public IElement FindElement(ILocator locator);
    public IEnumerable<IElement> FindElements(ILocator locator);
    public IElement FindParent();
    public IEnumerable<IElement> FindChildElements();
    public string GetAttribute(string attributeName);
    public string GetCssValue(string propertyName);
    public string GetDomAttribute(string attributeName);
    public string GetDomProperty(string propertyName);
};
