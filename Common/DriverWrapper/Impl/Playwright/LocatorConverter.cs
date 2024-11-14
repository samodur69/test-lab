namespace Common.DriverWrapper.Impl.Playwright;

public static class LocatorConverter
{
    public static string Convert(ILocator locator, Microsoft.Playwright.IPage page)
    {
        var selector = locator.Mechanism switch
        {
            LocatorTypes.ID => $"#{locator.Criteria}",
            LocatorTypes.LINK_TEXT => $"text={locator.Criteria}",
            LocatorTypes.NAME => $"[name='{locator.Criteria}']",
            LocatorTypes.XPATH => $"xpath={locator.Criteria}",
            LocatorTypes.CLASS_NAME => $".{locator.Criteria}",
            LocatorTypes.PARTIAL_LINK_TEXT => $"text={locator.Criteria}",
            LocatorTypes.TAG_NAME => locator.Criteria,
            LocatorTypes.CSS_SELECTOR => $"css={locator.Criteria}",
            _ => throw new ArgumentException("This Playwright locator is not implemented!", $"{locator.Mechanism}")
        };

        return selector;
    }
}