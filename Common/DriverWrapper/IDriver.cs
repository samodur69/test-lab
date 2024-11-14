namespace Common.DriverWrapper;

public interface IDriver
{
    /// <summary>
    ///     Finds an element according to the default or a custom waiting strategy
    /// </summary>
    /// <param name="xpath">Valid XPATH selector</param>
    /// <param name="waitingStrategy">Waiting strategy - DISPLAYED/ENABLED/TEXT/ATTRIBUTE</param>
    /// <param name="customArg0">Text when using WaitingStrategy.TEXT</param>
    /// <param name="customArg1">Attribute's value when using WaitingStrategy.ATTRIBUTE</param>
    /// <returns>
    ///     Returns element that is visible. Throws an exception after the wait timer expires.
    /// </returns>
    public IElement FindElementByCss(string css, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "");
    /// <summary>
    ///     Finds an element according to the default or a custom waiting strategy
    /// </summary>
    /// <param name="xpath">Valid XPATH selector</param>
    /// <param name="waitingStrategy">Waiting strategy - DISPLAYED/ENABLED/TEXT/ATTRIBUTE</param>
    /// <param name="customArg0">Text when using WaitingStrategy.TEXT</param>
    /// <param name="customArg1">Attribute's value when using WaitingStrategy.ATTRIBUTE</param>
    /// <returns>
    ///     Returns element that is visible. Throws an exception after the wait timer expires.
    /// </returns>
    public IElement FindElementByXPath(string xpath, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "");
    /// <summary>
    ///     Finds an element according to the default or a custom waiting strategy
    /// </summary>
    /// <param name="locator">Valid ILocator selector</param>
    /// <param name="waitingStrategy">Waiting strategy - DISPLAYED/ENABLED/TEXT/ATTRIBUTE</param>
    /// <param name="customArg0">Text when using WaitingStrategy.TEXT</param>
    /// <param name="customArg1">Attribute's value when using WaitingStrategy.ATTRIBUTE</param>
    /// <returns>
    ///     Returns element that is visible. Throws an exception after the wait timer expires.
    /// </returns>
    public IElement FindElement(ILocator locator, WaitingStrategy waitingStrategy = WaitingStrategy.NONE, string customArg0 = "", string customArg1 = "");
    /// <summary>
    ///     Finds all elements matching the default or a custom waiting strategy
    /// </summary>
    /// <param name="locator">Valid ILocator selector</param>
    /// <returns>
    ///     Returns available elements. Returns an empty collection if nothing was found.
    /// </returns>
    public IEnumerable<IElement> FindElements(ILocator locator);
    public IEnumerable<IElement> FindElementsByCss(string css);
    public IEnumerable<IElement> FindElementsByXPath(string xpath);
    public void GoToURL(string url);
    public string GetURL();
    public void ScrollDown(int byAmount = -1);
    public bool SwitchToNextTab();
    public string TakeScreenshot(string testName = "");
    public ICollection<System.Net.Cookie> GetCookies();
    public void Refresh();
    public void GoBack();
    public void Close();
    public void Click(System.Drawing.Point pos);
    public void Click(int x, int y);
    public void ClickElement(IElement element);
    public void RightClickElement(IElement element);
    public void DoubleClickElement(IElement element);
    public void DoubleClick(System.Drawing.Point pos);
    public void DoubleClick(int x, int y);
    public void MoveCursorToElement(IElement element);
    public void DragAndDropToOffset(IElement element, int x, int y);
    public void MoveCursorTo(System.Drawing.Point pos);
    public void MoveCursorTo(int x, int y);
    public System.Drawing.Point GetElementPos(IElement element);
};
