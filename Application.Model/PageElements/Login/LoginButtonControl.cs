namespace Application.Model.PageElements.Login;
using Common.DriverWrapper;

public class LoginButtonControl(IElement root) : ButtonControl(root)
{
    public override void Click() => Root.Click();
}