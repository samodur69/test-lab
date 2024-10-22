namespace Application.Model.Pages;

using Application.Model.PageElements;
using Application.Model.PageElements.Login;
using Common.Configuration;

public class HomePage : ModelBase
{
    private const string LoginBtnCSS = "button[data-testid='login-button']";
    private const string UserProfileBtnCSS = "button[data-testid='user-widget-link']";
    public HomePage()
    {
        Url = AppConfig.Url.Base;
    }
    
    public static LoginButtonControl LoginButton => new (Driver.FindElementByCss(LoginBtnCSS));
    public static ButtonControl UserProfileButton => new (Driver.FindElementByCss(UserProfileBtnCSS));
}
