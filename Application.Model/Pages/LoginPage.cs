namespace Application.Model.Pages;

using Application.Model.PageElements;
using Common.Configuration;
using Common.DriverWrapper;

public class LoginPage : ModelBase
{
    private const string UsernameField = "input[id='login-username']";
    private const string PasswordField = "input[id='login-password']";
    private const string LoginBtn = "button[id='login-button']";
    private const string ErrorBannerDiv = "div[data-encore-id='banner']";
    public LoginPage()
    {
        Url = AppConfig.Url.Base + AppConfig.Url.Login;
    }
    public InputControl Username => new (Driver.FindElementByCss(UsernameField));
    public InputControl Password => new (Driver.FindElementByCss(PasswordField));
    public ButtonControl LoginButton => new (Driver.FindElementByCss(LoginBtn));
    public ModelControlBase ErrorBanner => new (Driver.FindElementByCss(ErrorBannerDiv));
}
