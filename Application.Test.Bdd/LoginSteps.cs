namespace Application.Bdd;

using Business.Pages;

using FluentAssertions;
using TechTalk.SpecFlow;

[Binding]
public class LoginSteps(ScenarioContext context) : BaseStep(context)
{
    private static readonly string _loginEmail = AppConfig.EnvironmentVariables.Email;
    private static readonly string _loginPassword = AppConfig.EnvironmentVariables.Password;
    private readonly Home _homePage = new();
    private readonly Login _loginPage = new();

    [Given("I open the home page")]
    public void GivenIOpenTheHomePage()
    {
        _homePage.Open();
    }

    [Given("I click the Login button")]
    public void GivenIClickTheLoginButton()
    {
        _homePage.PressLogin();
    }

    [Then("I closed the cookies")]
    public void ThenIClosedTheCookies()
    {
        _homePage.CloseCookies();
    }

    [Then("I should be on the login page")]
    public void ThenIShouldBeOnTheLoginPage()
    {
        _loginPage.IsLoginTitleVisible().Should().BeTrue("Login title should be visible.");
    }

    [Then("I should see the login options")]
    public void ThenIShouldSeeTheLoginOptions()
    {
        _loginPage.IsLoginTitleVisible().Should().BeTrue("Login title should be visible.");
        _loginPage.IsLoginWithGoogleVisible().Should().BeTrue("Google login option should be visible.");
        _loginPage.IsLoginWithFaceBookVisible().Should().BeTrue("Facebook login option should be visible.");
        _loginPage.IsLoginWithAppleVisible().Should().BeTrue("Apple login option should be visible.");
    }

    [When("I log in with valid credentials")]
    public void WhenILogInWithEmailAndPassword()
    {
        _loginPage.EnterUsername(_loginEmail);
        _loginPage.EnterPassword(_loginPassword);
        _loginPage.PressLogin();
    }

    [Then("I should be successfully logged in")]
    public void ThenIShouldBeSuccessfullyLoggedIn()
    {
        _homePage.IsUserProfileButtonVisible().Should().BeTrue("User profile button should be visible after successful login.");
    }
}