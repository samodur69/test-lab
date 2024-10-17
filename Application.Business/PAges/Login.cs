namespace Application.Business.Pages;

using Application.Model.Pages;

public class Login : BusinessBase
{
    private LoginPage Page => (LoginPage)BaseModel;
    public Login() : base(new LoginPage()){}
    public Login(LoginPage loginPage) : base(loginPage){}
    public void EnterUsername(string username) 
    { 
        Page.Username.Text = username;
        WaitForDomProperty(Page.Username, "value", username);
    }
    public void EnterPassword(string password) 
    { 
        Page.Password.Text = password;
        WaitForDomProperty(Page.Password, "value", password);
    }
    public void PressLogin() => Page.LoginButton.Click();
    public bool IsErrorBannerVisible() => Page.ErrorBanner.IsVisible;
}
