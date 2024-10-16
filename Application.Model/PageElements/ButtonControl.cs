using Common.DriverWrapper;

namespace Application.Model.PageElements;

public class ButtonControl(IElement Root) : ModelControlBase(Root)
{
    public void Click() => Root.Click();
}