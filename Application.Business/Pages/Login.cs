namespace Application.Business.Pages;

using Application.Model.Pages;
using Application.Business.PageElements.Login;

public class Login : BusinessBase
{
    private LoginUsernameInput UsernameInput => new(LoginPage.Username);
    private LoginPasswordInput PasswordInput => new(LoginPage.Password);
    private LoginButton LoginButton => new(LoginPage.LoginButton);
    private LoginErrorBanner LoginErrorBanner => new(LoginPage.LoginErrorBanner);
    public Login() : base(new LoginPage()){}
    public Login(LoginPage loginPage) : base(loginPage){}
    public void EnterUsername(string username) => UsernameInput.Text = username;
    public void ClearUsername() => UsernameInput.Clear();
    public void EnterPassword(string password) => PasswordInput.Text = password;
    public void ClearPassword() => PasswordInput.Clear();
    public void PressLogin() => LoginButton.Click();
    public void WaitLoginButtonInvalid() => LoginButton.WaitInvalid();
    public void WaitLoginButtonValid() => LoginButton.WaitValid();
    public bool IsEmptyUsernameErrorVisible() => !UsernameInput.IsValidData();
    public bool IsEmptyPasswordErrorVisible() => !PasswordInput.IsValidData();
    public bool IsErrorBannerVisible() => LoginErrorBanner.IsError();
    public bool IsLoginTitleVisible() => LoginPage.LoginTitle.IsVisible;
    public bool IsLoginWithGoogleVisible() => LoginPage.LoginWithGoogleButton.IsVisible;
    public bool IsLoginWithFaceBookVisible() => LoginPage.LoginWithFaceBookButton.IsVisible;
    public bool IsLoginWithAppleVisible() => LoginPage.LoginWithAppleButton.IsVisible;
}
