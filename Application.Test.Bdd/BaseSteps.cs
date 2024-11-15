namespace Application.Bdd;

using TechTalk.SpecFlow;

using Common.Configuration;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using Application.Business;

[TestFixture]
public abstract class BaseStep(ScenarioContext context)
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected readonly ScenarioContext ScenarioContext = context;

    [BeforeScenario]
    public void BeforeScenario(TestContext testContext)
    {
        //
    }

    [AfterScenario]
    public void AfterScenario(TestContext testContext)
    {
        if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            BusinessBase.TakeScreenshot();
        } 
        BusinessBase.CloseBrowser();
    }
}