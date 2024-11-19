using Application.Business.Pages;

namespace Application.Test;

[TestFixture]
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

    [Test, TestCaseSource(nameof(LoginTestCases.WrongUsernameAndEmailWrongPassword))]
    [Description("Login with Wrong Email or Wrong Username and Wrong Password")]
    [Category("Extended")]
    [Property("Priority", "P2")]
    public void LoginPage_Should_Fail_When_InvalidUsername_And_InvalidPassword_AreProvided(string username, string password) => 
        LoginPage_Should_Fail_When_ValidUsername_And_InvalidPassword_AreProvided(username, password);

    [Test, TestCaseSource(nameof(LoginTestCases.ValidEmailUsername))]
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

    [Test, TestCaseSource(nameof(LoginTestCases.ValidPassword))]
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

        return new Pages(homePage.PressLogin(), homePage);
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

        return new Pages(loginPage, homePage);
    }

    private static IEnumerable<TestCaseData> ValidEmailPassword(){
        yield return new TestCaseData(AppConfig.EnvironmentVariables.Email, AppConfig.EnvironmentVariables.Password);
    }

    private static IEnumerable<TestCaseData> ValidUsernamePassword(){
        yield return new TestCaseData(AppConfig.EnvironmentVariables.Username, AppConfig.EnvironmentVariables.Password);
    }

    private static IEnumerable<TestCaseData> ValidUsernameAndEmailWrongPassword(){
        yield return new TestCaseData(AppConfig.EnvironmentVariables.Username, "reallywrongpswd@@@aa");
        yield return new TestCaseData(AppConfig.EnvironmentVariables.Email, "reallywrongpswd");
        yield return new TestCaseData(AppConfig.EnvironmentVariables.Username, "1929073h5ikash@a");
        yield return new TestCaseData(AppConfig.EnvironmentVariables.Email, "1929073h5ikash**444");
    }
}