using Application.Model.PageElements.Login;

namespace Application.Business.PageElements.Login;

public class LoginButton(LoginButtonControl model) : Button(model)
{
    public override void Click() => model.Click();
}