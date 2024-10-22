namespace Application.Model.Pages;

using Application.Model.PageElements;
using Application.Model.PageElements.Login;
using Common.Configuration;

public class LoginPage : ModelBase
{
    private const string UsernameFieldCSS = "input[id='login-username']";
    private const string PasswordFieldCSS = "input[id='login-password']";
    private const string LoginBtnCSS = "button[id='login-button']";
    private const string ErrorBannerDivCSS = "div[data-encore-id='banner']";
    private const string LoginWithGoogleCSS = "button[data-testid='google-login']";
    private const string LoginWithFaceBookCSS = "button[data-testid='facebook-login']";
    private const string LoginWithAppleCSS = "button[data-testid='apple-login']";
    private const string LoginLabelCSS = "div[data-testid='login-container'] h1[variant='titleLarge']";
    public LoginPage()
    {
        Url = AppConfig.Url.Base + AppConfig.Url.Login;
    }
    public static LoginUsernameInputControl Username => new (Driver.FindElementByCss(UsernameFieldCSS));
    public static LoginPasswordInputControl Password => new (Driver.FindElementByCss(PasswordFieldCSS));
    public static LoginButtonControl LoginButton => new (Driver.FindElementByCss(LoginBtnCSS));
    public static LoginErrorBannerControl LoginErrorBanner => new (Driver.FindElementByCss(ErrorBannerDivCSS));
    public static ModelControlBase LoginTitle => new (Driver.FindElementByCss(LoginLabelCSS));
    public static ButtonControl LoginWithGoogleButton => new (Driver.FindElementByCss(LoginWithGoogleCSS));
    public static ButtonControl LoginWithFaceBookButton => new (Driver.FindElementByCss(LoginWithFaceBookCSS));
    public static ButtonControl LoginWithAppleButton => new (Driver.FindElementByCss(LoginWithAppleCSS));
}
