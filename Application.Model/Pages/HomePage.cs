namespace Application.Model.Pages;

using Application.Model.PageElements;
using Common.Configuration;
using Common.DriverWrapper;

public class HomePage : ModelBase
{
    private const string LoginBtn = "button[data-testid='login-button']";
    public HomePage()
    {
        Url = AppConfig.Url.Base;
    }
    
    public ButtonControl LoginButton => new (Driver.FindElementByCss(LoginBtn));

    //Examples of usage:
    //public ButtonControl LoginButton => new (Driver.FindElementByCss(LoginBtn, WaitingStrategy.TEXT, "Log in"));
    //public ButtonControl LoginButton => new (Driver.FindElementByCss(LoginBtn, WaitingStrategy.ATTRIBUTE, "data-encore-id", "buttonPrimary"));
}
