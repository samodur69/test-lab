namespace Application.Test;

using Business.Pages;

[TestFixture]
//[Parallelizable(ParallelScope.All)]
public class LoginTests : TestFixtureBase
{
    [Test, TestCaseSource(nameof(ValidEmailPassword))]
    [Description("Login with Valid Email and Valid Password")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void LoginPage_Should_SuccessfullyLogin_When_ValidEmailAndPassword_AreProvided(string email, string password)
    {
        var pages = LoginBaseStep(email, password);
        pages.Home.IsUserProfileButtonVisible().Should().BeTrue("This element is visible when the login flow was successful");
    }

    [Test, TestCaseSource(nameof(ValidUsernamePassword))]
    [Description("Login with Valid Username and Valid Password")]
    [Category("Smoke")]
    [Property("Priority", "P1")]
    public void LoginPage_Should_SuccessfullyLogin_When_ValidUserNameAndPassword_AreProvided(string username, string password) => 
        LoginPage_Should_SuccessfullyLogin_When_ValidEmailAndPassword_AreProvided(username, password);

    [Test, TestCaseSource(nameof(ValidUsernameAndEmailWrongPassword))]
    [Description("Login with Valid Email or Valid Username but Wrong Password")]
    [Category("Extended")]
    [Property("Priority", "P2")]
    public void LoginPage_Should_Fail_When_ValidUsername_And_InvalidPassword_AreProvided(string username, string password)
    {
        var pages = LoginBaseStep(username, password);
        pages.Login.IsErrorBannerVisible().Should().BeTrue();
    }

    [Test, TestCaseSource(nameof(WrongUsernameAndEmailWrongPassword))]
    [Description("Login with Wrong Email or Wrong Username and Wrong Password")]
    [Category("Extended")]
    [Property("Priority", "P2")]
    public void LoginPage_Should_Fail_When_InvalidUsername_And_InvalidPassword_AreProvided(string username, string password) => 
        LoginPage_Should_Fail_When_ValidUsername_And_InvalidPassword_AreProvided(username, password);

    [Test, TestCaseSource(nameof(ValidEmailUsername))]
    [Description("Error Message under Empty Email Field")]
    [Category("Extended")]
    [Property("Priority", "P3")]
    public void LoginPage_Should_DisplayErrorMessage_When_EmailOrUsername_AreEmpty(string username)
    {
        var pages = HomeBaseStep();

        pages.Login.EnterUsername(username);
        pages.Login.ClearUsername();
        pages.Login.IsEmptyUsernameErrorVisible().Should().BeTrue();
    }

    [Test, TestCaseSource(nameof(ValidPassword))]
    [Description("Error Message under Empty Password Field")]
    [Category("Extended")]
    [Property("Priority", "P3")]
    public void LoginPage_Should_DisplayErrorMessage_When_Password_IsEmpty(string password)
    {
        var pages = HomeBaseStep();

        pages.Login.EnterPassword(password);
        pages.Login.ClearPassword();
        pages.Login.IsEmptyPasswordErrorVisible().Should().BeTrue();
    }

    private record Pages(Login Login, Home Home);

    private static Pages HomeBaseStep()
    {
        var homePage = new Home();
        homePage.Open();

        return new(homePage.PressLogin(), homePage);
    }

    private static Pages LoginBaseStep(string email, string password)
    {
        var (loginPage, homePage) = HomeBaseStep();

        loginPage.IsLoginTitleVisible().Should().BeTrue();

        loginPage.IsLoginWithGoogleVisible().Should().BeTrue();
        loginPage.IsLoginWithFaceBookVisible().Should().BeTrue();
        loginPage.IsLoginWithAppleVisible().Should().BeTrue();

        loginPage.EnterUsername(email);
        loginPage.EnterPassword(password);
        loginPage.PressLogin();

        return new(loginPage, homePage);
    }

    private static IEnumerable<TestCaseData> ValidEmailPassword(){
        yield return new(AppConfig.EnvironmentVariables.Email, AppConfig.EnvironmentVariables.Password);
    }

    private static IEnumerable<TestCaseData> ValidUsernamePassword(){
        yield return new(AppConfig.EnvironmentVariables.Username, AppConfig.EnvironmentVariables.Password);
    }

    private static IEnumerable<TestCaseData> ValidUsernameAndEmailWrongPassword(){
        yield return new(AppConfig.EnvironmentVariables.Username, "reallywrongpswd@@@aa");
        yield return new(AppConfig.EnvironmentVariables.Email, "reallywrongpswd");
        yield return new(AppConfig.EnvironmentVariables.Username, "1929073h5ikash@a");
        yield return new(AppConfig.EnvironmentVariables.Email, "1929073h5ikash**444");
    }

    private static IEnumerable<TestCaseData> WrongUsernameAndEmailWrongPassword(){
        yield return new("SfA5aS1YFEJKnTHNuusChXsSNJhA", "@323sss13");
        yield return new("wronk_email@email.com", "$$2391470214aa");
        yield return new("somethingsomething@bad.com", "$$2391470214aa");
        yield return new("90123745sChXsS9132phPHWE3h6", "$$2391470214aa");
    }

    private static IEnumerable<TestCaseData> ValidEmailUsername(){
        yield return new("SfA5aS1YFEJKnTHNuusChXsSNJhA");
        yield return new("wronk_email@email.com");
    }

    private static IEnumerable<TestCaseData> ValidPassword(){
        yield return new("2139128@$!$90");
        yield return new("2194y21ihS*YQ389i6o36");
        yield return new("2391470214aa");
        yield return new("1902354upoJS(Y40po2n789)");
    }
}