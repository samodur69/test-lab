namespace Common.DriverWrapper.Impl.Selenium;

public class SeleniumLocator : ILocator
{
    public SeleniumLocator(){}
    public SeleniumLocator(LocatorTypes mechanism, string criteria) {
        Mechanism = mechanism;
        Criteria = criteria;
    }
    public LocatorTypes Mechanism { get; } = LocatorTypes.CSS_SELECTOR;
    public string Criteria { get; } = "";
    public ILocator Id(string idToFind) => new SeleniumLocator(LocatorTypes.ID, idToFind);
    public ILocator LinkText(string linkTextToFind) => new SeleniumLocator(LocatorTypes.LINK_TEXT, linkTextToFind);
    public ILocator Name(string nameToFind) => new SeleniumLocator(LocatorTypes.NAME, nameToFind);
    public ILocator XPath(string xpathToFind) => new SeleniumLocator(LocatorTypes.XPATH, xpathToFind);
    public ILocator ClassName(string classNameToFind) => new SeleniumLocator(LocatorTypes.CLASS_NAME, classNameToFind);
    public ILocator PartialLinkText(string partialLinkTextToFind) => new SeleniumLocator(LocatorTypes.PARTIAL_LINK_TEXT, partialLinkTextToFind);
    public ILocator TagName(string tagNameToFind) => new SeleniumLocator(LocatorTypes.TAG_NAME, tagNameToFind);
    public ILocator CssSelector(string cssSelectorToFind) => new SeleniumLocator(LocatorTypes.CSS_SELECTOR, cssSelectorToFind);
};