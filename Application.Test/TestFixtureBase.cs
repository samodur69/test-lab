namespace Application.Test;

using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;
using Business.Pages;

[TestFixture]
public abstract class TestFixtureBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected static readonly ILogger Logger = LoggerFactory.Create(
        new LoggerConfig(AppConfig.LoggerOptions.Type, AppConfig.LoggerOptions.FileName)
    );

    protected readonly Dictionary<string, string> TestData;

    //Example of a data provider. Could parse a configuration file to fetch test data.
    protected static IEnumerable<Dictionary<string, string>> TestCaseDatas()
    {
        yield return new Dictionary<string, string> 
        { 
            { "browser", AppConfig.BrowserOptions.Browser }, 
            { "username", AppConfig.Credentials.Username }, 
            { "password", AppConfig.Credentials.Password }
        };
    }

    protected TestFixtureBase(Dictionary<string, string> data)
    {
        TestData = data;
    }

    [TearDown]
    public void OnTearDown()
    {
        // if(TestContext.CurrentContext.Result.Outcome != ResultState.Success)
        //     Home.TakeScreenshot();
        Home.CloseBrowser();
    }
}
