namespace Common.DriverWrapper.Impl.Playwright;

public class PlaywrightLocator : ILocator
{
    public PlaywrightLocator() { }

    public PlaywrightLocator(LocatorTypes mechanism, string criteria)
    {
        Mechanism = mechanism;
        Criteria = criteria;
    }

    public LocatorTypes Mechanism { get; } = LocatorTypes.CSS_SELECTOR;
    public string Criteria { get; } = "";

    public ILocator Id(string idToFind) => new PlaywrightLocator(LocatorTypes.ID, idToFind);
    public ILocator LinkText(string linkTextToFind) => new PlaywrightLocator(LocatorTypes.LINK_TEXT, linkTextToFind);
    public ILocator Name(string nameToFind) => new PlaywrightLocator(LocatorTypes.NAME, nameToFind);
    public ILocator XPath(string xpathToFind) => new PlaywrightLocator(LocatorTypes.XPATH, xpathToFind);
    public ILocator ClassName(string classNameToFind) => new PlaywrightLocator(LocatorTypes.CLASS_NAME, classNameToFind);
    public ILocator PartialLinkText(string partialLinkTextToFind) => new PlaywrightLocator(LocatorTypes.PARTIAL_LINK_TEXT, partialLinkTextToFind);
    public ILocator TagName(string tagNameToFind) => new PlaywrightLocator(LocatorTypes.TAG_NAME, tagNameToFind);
    public ILocator CssSelector(string cssSelectorToFind) => new PlaywrightLocator(LocatorTypes.CSS_SELECTOR, cssSelectorToFind);
}