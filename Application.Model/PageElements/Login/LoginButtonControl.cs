using Common.DriverWrapper;

namespace Application.Model.PageElements.Login;

public class LoginButtonControl(IElement root) : ButtonControl(root)
{
    public override void Click() => Root.Click();
}