namespace Common.DriverWrapper;

public interface ILocator
{
    LocatorTypes Mechanism { get; }
    string Criteria { get; }
    ILocator Id(string idToFind);
    ILocator LinkText(string linkTextToFind);
    ILocator Name(string nameToFind);
    ILocator XPath(string xpathToFind);
    ILocator ClassName(string classNameToFind);
    ILocator PartialLinkText(string partialLinkTextToFind);
    ILocator TagName(string tagNameToFind);
    ILocator CssSelector(string cssSelectorToFind);
};