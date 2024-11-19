using Application.Business;
using Application.Business.Pages;
using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;
using NUnit.Framework.Interfaces;
using ReportPortal.Shared;

namespace Application.Test;

[TestFixture]
public abstract class TestFixtureBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected static readonly ILogger Logger = LoggerFactory.Create(new LoggerConfig(
        AppConfig.LoggerOptions.Type,
        AppConfig.LoggerOptions.FileName)
    );

    [TearDown]
    public void OnTearDown()
    {
        if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            Context.Current.Log.Info($"{TestContext.CurrentContext.Test.FullName} Attachment", "image/png", File.ReadAllBytes(BusinessBase.TakeScreenshot()));
        } 
        Home.CloseBrowser();
    }
}
