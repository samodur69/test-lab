namespace Application.Test;

using Common.Configuration;
using Common.Logger;
using Common.Logger.Configuration;
using Business.Pages;
using NUnit.Framework.Interfaces;
using Application.Business;

using System.IO;

[TestFixture]
public abstract class TestFixtureBase
{
    protected static readonly AppConfig AppConfig = ConfigurationManager.AppConfig;
    protected static readonly ILogger Logger = LoggerFactory.Create(
        new LoggerConfig(AppConfig.LoggerOptions.Type, AppConfig.LoggerOptions.FileName)
    );

    [TearDown]
    public void OnTearDown()
    {
        if(TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            ReportPortal.Shared.Context.Current.Log.Info($"{TestContext.CurrentContext.Test.FullName} Attachment", "image/png", File.ReadAllBytes(BusinessBase.TakeScreenshot()));
        } 
        Home.CloseBrowser();
    }

}
