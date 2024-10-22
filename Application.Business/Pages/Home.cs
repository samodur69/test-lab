namespace Application.Business.Pages;

using Application.Model.Pages;
using Application.Business.PageElements.Login;
using Application.Model;

public class Home : BusinessBase
{
    private LoginButton LoginButton => new(HomePage.LoginButton);
    public Home() : base(new HomePage()){}
    public Home(HomePage homePage) : base(homePage){}
    public Login PressLogin() {
        LoginButton.Click();
        return new Login();
    }
    public bool IsUserProfileButtonVisible() => HomePage.UserProfileButton.IsVisible;
    public bool IsLoginButtonInvalid() => LoginButton.WaitInvalid(ModelBase.DEFAULT_TIMEOUT);
}
