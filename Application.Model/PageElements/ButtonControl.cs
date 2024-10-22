using Common.DriverWrapper;

namespace Application.Model.PageElements;

public class ButtonControl(IElement root) : BaseElementControl(root)
{
    public virtual void Click() => Root.Click();
}