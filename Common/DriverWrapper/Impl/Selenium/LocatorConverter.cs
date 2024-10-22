namespace Common.DriverWrapper.Impl.Selenium;

using OpenQA.Selenium;

public static class LocatorConverter
{
    public static By Convert(ILocator locator) {
        return locator.Mechanism switch
        {
            LocatorTypes.ID                => By.Id(locator.Criteria),
            LocatorTypes.LINK_TEXT         => By.LinkText(locator.Criteria),
            LocatorTypes.NAME              => By.Name(locator.Criteria),
            LocatorTypes.XPATH             => By.XPath(locator.Criteria),
            LocatorTypes.CLASS_NAME        => By.ClassName(locator.Criteria),
            LocatorTypes.PARTIAL_LINK_TEXT => By.PartialLinkText(locator.Criteria),
            LocatorTypes.TAG_NAME          => By.TagName(locator.Criteria),
            LocatorTypes.CSS_SELECTOR      => By.CssSelector(locator.Criteria),

            _ => throw new ArgumentException("This Selenium locator is not implemented!", $"{locator.Mechanism}")
        };
    }
}