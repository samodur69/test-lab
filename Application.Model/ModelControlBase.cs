using Common.DriverWrapper;
using Common.Utils.Waiter;

namespace Application.Model;

public class ModelControlBase(IElement root) : ModelBase
{
    protected IElement Root = root;
    public bool IsVisible{ get; } = root.Displayed;
    public bool IsEnabled{ get; } = root.Enabled;
    public bool IsSelected{ get; } = root.Selected;
    public IElement element => Root;
    public bool ElementDomPropEqual(string prop, string value, bool condition = true, int timeout = 0) => 
        BaseElementCondition((object[]? args) => GetDomProperty((string)args![0]) == (string)args[1], condition, timeout, prop, value);
    public bool ElementAttributeEqual(string attrib, string value, bool condition = true, int timeout = 0) => 
        BaseElementCondition((object[]? args) => GetAttribute((string)args![0]) == (string)args[1], condition, timeout, attrib, value);
    public bool ElementEnabled(bool condition = true, int timeout = 0) => BaseElementCondition(() => IsEnabled, condition, timeout);
    public bool ElementVisible(bool condition = true, int timeout = 0) => BaseElementCondition(() => IsVisible, condition, timeout);
    public bool ElementSelected(bool condition = true, int timeout = 0) => BaseElementCondition(() => IsSelected, condition, timeout);
    public bool ElementValid(bool condition = true, int timeout = 0) => BaseElementCondition(IsValid, condition, timeout);
    public bool IsValid() => Driver.FindElements(Root.Locator).Any();
    private string GetAttribute(string attributeName) => Root.GetAttribute(attributeName);
    private string GetDomProperty(string propertyName) => Root.GetDomProperty(propertyName);
    private bool BaseElementCondition(Func<bool> func, bool condition, int timeout) => BaseElementCondition((object[]? args) => func(), condition, timeout, null);

    private bool BaseElementCondition(Func<object[]?, bool> func, bool condition, int timeout, params object[]? args)
    {
        bool funcWrapper()
        {
            var res = func(args);
            return condition ? res : !res;
        }

        return (timeout > 0) ? 
        Waiter.WaitUntil(funcWrapper, timeout)
        :
        Waiter.WaitUntil(funcWrapper);
    }
    protected void WaitForDomProperty(string prop, string value, int timeout = 0)
    {
        if(!ElementDomPropEqual(prop, value, true, timeout))
            throw new TimeoutException($"Dom property's value '{prop}' did not match the expected: '{value}', was: '{GetDomProperty(prop)}'!");
    }

    protected void WaitForAttribute(string attrib, string value, int timeout = 0)
    {
        if(!ElementAttributeEqual(attrib, value, true, timeout))
            throw new TimeoutException($"Attribute's value '{attrib}' did not match the expected: '{value}', was: '{GetAttribute(attrib)}'!");
    }

    protected void WaitUntilEnabled(int timeout = 0)
    {
        if(!ElementEnabled(true, timeout))
            throw new TimeoutException($"Element is not enabled!");
    }

    protected void WaitUntilNotEnabled(int timeout = 0)
    {
        if(!ElementEnabled(false, timeout))
            throw new TimeoutException($"Element is enabled!");
    }
    protected void WaitUntilVisible(int timeout = 0)
    {
        if(!ElementVisible(true, timeout))
            throw new TimeoutException($"Element is not visible!");
    }

    protected void WaitUntilNotVisible(int timeout = 0)
    {
        if(!ElementVisible(false, timeout))
            throw new TimeoutException($"Element is visible!");
    }

    protected void WaitUntilSelected(int timeout = 0)
    {
        if(!ElementSelected(true, timeout))
            throw new TimeoutException($"Element is not selected!");
    }

    protected void WaitUntilNotSelected(int timeout = 0)
    {
        if(!ElementSelected(false, timeout))
            throw new TimeoutException($"Element is selected!");
    }

    protected void WaitUntilValid(int timeout)
    {
        if(!ElementValid(true, timeout))
            throw new TimeoutException($"Element is not valid!");
    }

    protected void WaitUntilNotValid(int timeout)
    {
        if(!ElementValid(false, timeout))
            throw new TimeoutException($"Element is still valid!");
    }
}
