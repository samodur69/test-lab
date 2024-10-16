namespace Application.Test;

using Business.Pages;

[TestFixtureSource(nameof(TestCaseDatas))]
[Parallelizable(ParallelScope.All)]
public class SpotifyTest(Dictionary<string, string> testData) : TestFixtureBase(testData)
{
    [Test]
    public void LoginBadCredentialsTest()
    {
        var homePage = new Home();
        homePage.Open();

        var loginPage = homePage.PressLogin();
        loginPage.EnterUsername(TestData["username"]);
        loginPage.EnterPassword(TestData["password"]);
        loginPage.PressLogin();

        loginPage.IsErrorBannerVisible().Should().Be(true);
    }
}