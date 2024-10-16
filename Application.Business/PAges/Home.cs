namespace Application.Business.Pages;

using Application.Model.Pages;

public class Home : BusinessBase
{
    private HomePage Page => (HomePage)BaseModel;
    public Home() : base(new HomePage()){}
    public Home(HomePage homePage) : base(homePage){}
    public Login PressLogin() {
        Page.LoginButton.Click();
        return new Login();
    }
}
